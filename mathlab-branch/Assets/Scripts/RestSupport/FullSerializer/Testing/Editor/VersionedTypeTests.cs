using System.Collections.Generic;
using FullSerializer;
using FullSerializer.Internal;
using NUnit.Framework;

namespace RestSupport.FullSerializer.Testing.Editor {
    [fsObject("1")]
    public struct VersionedModelV1 {
        public int a;
    }

    [fsObject("1", typeof(VersionedModelV1))]
    public struct VersionedModelDuplicateVersionString {
        public VersionedModelDuplicateVersionString(VersionedModelV1 model) { }
    }

    [fsObject("2", typeof(VersionedModelV1))]
    public struct VersionedModelMissingConstructor {
    }

    [fsObject("2", typeof(VersionedModelV1))]
    public struct VersionedModelV2 {
        //public VersionedModel_v2() { }
        public VersionedModelV2(VersionedModelV1 model) {
            b = model.a;
        }

        public int b;
    }


    // Type graph hierarchy
    //
    //      1_2abc_3
    //
    //        /|\
    //       / | \
    //      /  |  \
    //     /   |   \
    //    /    |    \
    //   /     |     \
    //
    //  1_2a  1_2b  1_2c
    //
    //   \     |     /
    //    \    |    /
    //     \   |   /
    //      \  |  /
    //       \ | /
    //        \|/
    //
    //         1
    //
    // Valid migration paths:
    //   1 -> 1_2a -> 1_2abc_3
    //   1 -> 1_2b -> 1_2abc_3
    //   1 -> 1_2c -> 1_2abc_3

    [fsObject("1")]
    struct ComplexModel1 { }

    [fsObject("1_2a", typeof(ComplexModel1))]
    struct ComplexModel12A { ComplexModel12A(ComplexModel1 m) { } }
    [fsObject("1_2b", typeof(ComplexModel1))]
    struct ComplexModel12B { ComplexModel12B(ComplexModel1 m) { } }
    [fsObject("1_2c", typeof(ComplexModel1))]
    struct ComplexModel12C { ComplexModel12C(ComplexModel1 m) { } }

    [fsObject("1_2abc_3", typeof(ComplexModel12A), typeof(ComplexModel12B), typeof(ComplexModel12C))]
    struct ComplexModel12Abc3 {
        ComplexModel12Abc3(ComplexModel12A m) { }
        ComplexModel12Abc3(ComplexModel12B m) { }
        ComplexModel12Abc3(ComplexModel12C m) { }
    }




    public class VersionedTypeTests {
        
        [Test]
        public void DuplicateVersionString() {
            fsVersionManager.GetVersionedType(typeof(VersionedModelDuplicateVersionString));
            Assert.Throws<fsDuplicateVersionNameException>(code: () => DuplicateVersionString());
        }

        [Test]
        public void MissingConstructor() {
            fsVersionManager.GetVersionedType(typeof(VersionedModelMissingConstructor));
            Assert.Throws<fsDuplicateVersionNameException>(code: () => MissingConstructor());
        }

        [Test]
        public void VerifyGraphHistory() {
            Assert.AreEqual(
                fsVersionManager.GetVersionedType(typeof(VersionedModelV1)).Value,
                fsVersionManager.GetVersionedType(typeof(VersionedModelV2)).Value.Ancestors[0]);

            List<fsVersionedType> path;


            Assert.IsTrue(fsVersionManager.GetVersionImportPath("1", fsVersionManager.GetVersionedType(typeof(VersionedModelV2)).Value, out path).Succeeded);
            Assert.AreEqual(2, path.Count);
            Assert.AreEqual(fsVersionManager.GetVersionedType(typeof(VersionedModelV1)).Value, path[0]);
            Assert.AreEqual(fsVersionManager.GetVersionedType(typeof(VersionedModelV2)).Value, path[1]);


            Assert.IsTrue(fsVersionManager.GetVersionImportPath("1", fsVersionManager.GetVersionedType(typeof(ComplexModel12Abc3)).Value, out path).Succeeded);
            Assert.AreEqual(3, path.Count);
            Assert.AreEqual(fsVersionManager.GetVersionedType(typeof(ComplexModel1)).Value, path[0]);
            Assert.IsTrue(
                (fsVersionManager.GetVersionedType(typeof(ComplexModel12A)).Value == path[1]) ||
                (fsVersionManager.GetVersionedType(typeof(ComplexModel12B)).Value == path[1]) ||
                (fsVersionManager.GetVersionedType(typeof(ComplexModel12C)).Value == path[1]));
            Assert.AreEqual(fsVersionManager.GetVersionedType(typeof(ComplexModel12Abc3)).Value, path[2]);
        }

        [Test]
        public void MultistageMigration() {
            var serializer = new fsSerializer();

            var modelV1 = new VersionedModelV1 {
                a = 3
            };
            fsData serialized;
            serializer.TrySerialize(modelV1, out serialized).AssertSuccessWithoutWarnings();

            var modelV2 = new VersionedModelV2();
            serializer.TryDeserialize(serialized, ref modelV2).AssertSuccessWithoutWarnings();
            Assert.AreEqual(modelV1.a, modelV2.b);
        }
    }
}