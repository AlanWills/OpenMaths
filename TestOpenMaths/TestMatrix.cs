using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenMaths;

namespace TestOpenMaths
{
    [TestClass]
    public class TestMatrix
    {
        #region Properties and Fields

        private static float[] identityFloats = new float[16]
        {
            1, 0, 0, 0,
            0, 1, 0, 0,
            0, 0, 1, 0,
            0, 0, 0, 1,
        };

        #endregion

        #region Constructor Tests

        [TestMethod]
        public void DefaultConstructor_CreatesIdentityMatrix()
        {
            Matrix matrix = new Matrix();
            Assert.IsTrue(CheckFloatBytesEqual(identityFloats, matrix.Values));
        }

        [TestMethod]
        public void FloatArrayConstructor()
        {
            float[] values = new float[16]
            {
                -0.05f, 12.9f, 0.12312f, 1211211,
                10, 0, -0, -10,
                1092038102, 129, 1, -1,
                12345.6789f, 98765.4321f, 1357.98642f, 2.46897531f,
            };
            Matrix matrix = new Matrix(values);

            Assert.IsTrue(CheckFloatBytesEqual(values, matrix.Values));
        }

        [TestMethod]
        public void MatrixCopyConstructor()
        {
            float[] values = new float[16]
            {
                -0.05f, 12.9f, 0.12312f, 1211211,
                10, 0, -0, -10,
                1092038102, 129, 1, -1,
                12345.6789f, 98765.4321f, 1357.98642f, 2.46897531f,
            };
            Matrix original = new Matrix(values);
            Matrix copy = new Matrix(original);

            Assert.IsTrue(CheckFloatBytesEqual(values, copy.Values));
            Assert.IsTrue(CheckFloatBytesEqual(original.Values, copy.Values));
        }

        #endregion

        #region Equality Tests

        [TestMethod]
        public void EqualityOperator_Reflexivity()
        {
            float[] values = new float[]
            {
                12, 10, 8, 6,
                1.1f, 2.2f, -2.2f, -1.1f,
                1, -1, 1.0f, -1.0f,
                0, 0, -0, -0,
            };

            Matrix firstMatrix = new Matrix(values);
#pragma warning disable CS1718 // Comparison made to same variable
            Assert.IsTrue(firstMatrix == firstMatrix);
#pragma warning restore CS1718 // Comparison made to same variable
        }

        [TestMethod]
        public void EqualityOperator_ReturnsTrueIfMatricesHaveSameValues()
        {
            float[] values = new float[]
            {
                12, 10, 8, 6,
                1.1f, 2.2f, -2.2f, -1.1f,
                1, -1, 1.0f, -1.0f,
                0, 0, -0, -0,
            };

            Matrix firstMatrix = new Matrix(values);
            Matrix secondMatrix = new Matrix(values);

            Assert.IsTrue(firstMatrix == secondMatrix);
        }

        [TestMethod]
        public void EqualityOperator_ReturnsFalseIfMatricesHaveDifferentValues()
        {
            float[] values = new float[]
            {
                12, 10, 8, 6,
                1.1f, 2.2f, -2.2f, -1.1f,
                1, -1, 1.0f, -1.0f,
                0, 0, -0, -0,
            };

            Matrix firstMatrix = new Matrix(values);

            values = new float[]
            {
                112, 110, 81, 61,
                1.1f, 2.22f, -2.2f, -1.1f,
                1, -12, 10.0f, -1.0f,
                10, 8, 4, 4,
            };
            Matrix secondMatrix = new Matrix(values);

            Assert.IsFalse(firstMatrix == secondMatrix);
        }

        [TestMethod]
        public void EqualsFunction_Reflexivity()
        {
            float[] values = new float[]
            {
                12, 10, 8, 6,
                1.1f, 2.2f, -2.2f, -1.1f,
                1, -1, 1.0f, -1.0f,
                0, 0, -0, -0,
            };

            Matrix firstMatrix = new Matrix(values);
            Assert.IsTrue(firstMatrix.Equals(firstMatrix));
        }

        [TestMethod]
        public void EqualsFunction_ReturnsTrueIfMatricesHaveSameValues()
        {
            float[] values = new float[]
            {
                12, 10, 8, 6,
                1.1f, 2.2f, -2.2f, -1.1f,
                1, -1, 1.0f, -1.0f,
                0, 0, -0, -0,
            };

            Matrix firstMatrix = new Matrix(values);
            Matrix secondMatrix = new Matrix(values);

            Assert.IsTrue(firstMatrix.Equals(secondMatrix));
        }

        [TestMethod]
        public void EqualsFunction_ReturnsFalseIfMatricesHaveDifferentValues()
        {
            float[] values = new float[]
            {
                12, 10, 8, 6,
                1.1f, 2.2f, -2.2f, -1.1f,
                1, -1, 1.0f, -1.0f,
                0, 0, -0, -0,
            };

            Matrix firstMatrix = new Matrix(values);

            values = new float[]
            {
                112, 110, 81, 61,
                1.1f, 2.22f, -2.2f, -1.1f,
                1, -12, 10.0f, -1.0f,
                10, 8, 4, 4,
            };
            Matrix secondMatrix = new Matrix(values);

            Assert.IsFalse(firstMatrix.Equals(secondMatrix));
        }

        [TestMethod]
        public void InequalityOperator_Reflexivity()
        {
            float[] values = new float[]
            {
                12, 10, 8, 6,
                1.1f, 2.2f, -2.2f, -1.1f,
                1, -1, 1.0f, -1.0f,
                0, 0, -0, -0,
            };

            Matrix firstMatrix = new Matrix(values);

#pragma warning disable CS1718 // Comparison made to same variable
            Assert.IsTrue(firstMatrix != firstMatrix);
#pragma warning restore CS1718 // Comparison made to same variable
        }

        [TestMethod]
        public void InequalityOperator_ReturnsFalseIfMatricesHaveSameValues()
        {
            float[] values = new float[]
            {
                12, 10, 8, 6,
                1.1f, 2.2f, -2.2f, -1.1f,
                1, -1, 1.0f, -1.0f,
                0, 0, -0, -0,
            };

            Matrix firstMatrix = new Matrix(values);
            Matrix secondMatrix = new Matrix(values);

            Assert.IsTrue(firstMatrix != secondMatrix);
        }

        [TestMethod]
        public void InequalityOperator_ReturnsTrueIfMatricesHaveDifferentValues()
        {
            float[] values = new float[]
            {
                12, 10, 8, 6,
                1.1f, 2.2f, -2.2f, -1.1f,
                1, -1, 1.0f, -1.0f,
                0, 0, -0, -0,
            };

            Matrix firstMatrix = new Matrix(values);

            values = new float[]
            {
                112, 110, 81, 61,
                1.1f, 2.22f, -2.2f, -1.1f,
                1, -12, 10.0f, -1.0f,
                10, 8, 4, 4,
            };
            Matrix secondMatrix = new Matrix(values);

            Assert.IsFalse(firstMatrix != secondMatrix);
        }

        #endregion

        #region Test Casting

        [TestMethod]
        public void CastingToFloatArray_GivesUnderlyingMatrixValues()
        {
            float[] values = new float[16]
            {
                12, 10, 8, 6,
                1.1f, 2.2f, -2.2f, -1.1f,
                1, -1, 1.0f, -1.0f,
                0, 0, -0, -0,
            };

            Matrix matrix = new Matrix(values);
            Assert.IsTrue(CheckFloatBytesEqual(values, (float[])matrix));
        }

        [TestMethod]
        public void ToFloatArray_GivesUnderlyingMatrixValues()
        {
            float[] values = new float[16]
            {
                12, 10, 8, 6,
                1.1f, 2.2f, -2.2f, -1.1f,
                1, -1, 1.0f, -1.0f,
                0, 0, -0, -0,
            };

            Matrix matrix = new Matrix(values);
            Assert.IsTrue(CheckFloatBytesEqual(values, matrix.ToFloatArray()));
        }

        [TestMethod]
        public void ChangingFloatArrayFromCasting_DoesntUpdateMatrix()
        {
            float[] values = new float[16]
            {
                12, 10, 8, 6,
                1.1f, 2.2f, -2.2f, -1.1f,
                1, -1, 1.0f, -1.0f,
                0, 0, -0, -0,
            };

            Matrix matrix = new Matrix(values);

            float[] output = (float[])matrix;
            output[0] = -1;

            Assert.IsFalse(CheckFloatBytesEqual(output, (float[])matrix));
        }

        #endregion

        #region Multiplication Tests

        [TestMethod]
        public void IdentityMultiplication_LeavesMatricesUnchanged()
        {
            float[] values = new float[16]
            {
                12, 10, 8, 6,
                1.1f, 2.2f, -2.2f, -1.1f,
                1, -1, 1.0f, -1.0f,
                0, 0, -0, -0,
            };

            Matrix matrix = new Matrix(values);
            matrix.Multiply(Matrix.Identity);
            Assert.IsTrue(CheckFloatBytesEqual(values, matrix.Values));

            Matrix output = Matrix.Multiply(matrix, Matrix.Identity);
            Assert.IsTrue(CheckFloatBytesEqual(values, output.Values));

            output = matrix * Matrix.Identity;
            Assert.IsTrue(CheckFloatBytesEqual(values, output.Values));
        }

        [TestMethod]
        public void MultiplyOperator()
        {
            float[] values = new float[16]
            {
                2, 1, 3, 1.5f,
                0.5f, 2.1f, 12, -6,
                -0.2f, 0, -1, 100,
                0.4f, 1, 2, 0.5f,
            };

            Matrix lhs = new Matrix(values);

            values = new float[16]
            {
                2, 1, 3, 1.5f,
                0.5f, 2.1f, 12, -6,
                -0.2f, 0, -1, 100,
                0.4f, 1, 2, 0.5f,
            };

            Matrix rhs = new Matrix(values);
            Matrix result = lhs * rhs;

            float[] expected = new float[16]
            {
                2, 1, 3, 1.5f,
                0.5f, 2.1f, 12, -6,
                -0.2f, 0, -1, 100,
                0.4f, 1, 2, 0.5f,
            };

            Assert.IsTrue(CheckFloatBytesEqual(expected, result.Values));
        }

        [TestMethod]
        public void MultiplyFunction_Static()
        {
            float[] values = new float[16]
            {
                2, 1, 3, 1.5f,
                0.5f, 2.1f, 12, -6,
                -0.2f, 0, -1, 100,
                0.4f, 1, 2, 0.5f,
            };

            Matrix lhs = new Matrix(values);

            values = new float[16]
            {
                2, 1, 3, 1.5f,
                0.5f, 2.1f, 12, -6,
                -0.2f, 0, -1, 100,
                0.4f, 1, 2, 0.5f,
            };

            Matrix rhs = new Matrix(values);
            Matrix result = Matrix.Multiply(lhs, rhs);

            float[] expected = new float[16]
            {
                2, 1, 3, 1.5f,
                0.5f, 2.1f, 12, -6,
                -0.2f, 0, -1, 100,
                0.4f, 1, 2, 0.5f,
            };

            Assert.IsTrue(CheckFloatBytesEqual(expected, result.Values));
        }

        [TestMethod]
        public void MultiplyFunction_Instance()
        {
            float[] values = new float[16]
            {
                2, 1, 3, 1.5f,
                0.5f, 2.1f, 12, -6,
                -0.2f, 0, -1, 100,
                0.4f, 1, 2, 0.5f,
            };

            Matrix lhs = new Matrix(values);

            values = new float[16]
            {
                2, 1, 3, 1.5f,
                0.5f, 2.1f, 12, -6,
                -0.2f, 0, -1, 100,
                0.4f, 1, 2, 0.5f,
            };

            Matrix rhs = new Matrix(values);
            lhs.Multiply(rhs);

            float[] expected = new float[16]
            {
                2, 1, 3, 1.5f,
                0.5f, 2.1f, 12, -6,
                -0.2f, 0, -1, 100,
                0.4f, 1, 2, 0.5f,
            };

            Assert.IsTrue(CheckFloatBytesEqual(expected, lhs.Values));
        }

        #endregion

        #region Transpose Tests

        [TestMethod]
        public void TransposeDiagonal_Static()
        {
            float[] values = new float[16]
            {
                12, 0, 1, 0,
                0, 0, 3, 0,
                1, 3, -1.4f, 2,
                0, 0, 2, 66,
            };

            Matrix matrix = new Matrix(values);
            Matrix result = Matrix.Transpose(matrix);
            
            Assert.IsTrue(CheckFloatBytesEqual(values, result.Values));
        }

        [TestMethod]
        public void TransposeDiagonal_Instance()
        {
            float[] values = new float[16]
            {
                12, 0, 1, 0,
                0, 0, 3, 0,
                1, 3, -1.4f, 2,
                0, 0, 2, 66,
            };

            Matrix matrix = new Matrix(values);
            matrix.Transpose();
            
            Assert.IsTrue(CheckFloatBytesEqual(values, matrix.Values));
        }

        [TestMethod]
        public void TransposeNonDiagonal_Static()
        {
            float[] values = new float[16]
            {
                12, 10, 8, 6,
                1.1f, 2.2f, -2.2f, -1.1f,
                1, -1, 1.0f, -1.0f,
                0, 0, -0, -0,
            };

            Matrix matrix = new Matrix(values);
            Matrix result = Matrix.Transpose(matrix);

            float[] expected = new float[16]
            {
                12, 1.1f, 1, 0,
                10, 2.2f, -1, 0,
                8, -2.2f, 1.0f, -0,
                6, -1.1f, -1.0f, -0,
            };

            Assert.IsTrue(CheckFloatBytesEqual(expected, matrix.Values));
        }

        [TestMethod]
        public void TransposeNonDiagonal_Instance()
        {
            float[] values = new float[16]
            {
                12, 10, 8, 6,
                1.1f, 2.2f, -2.2f, -1.1f,
                1, -1, 1.0f, -1.0f,
                0, 0, -0, -0,
            };

            Matrix matrix = new Matrix(values);
            matrix.Transpose();

            float[] expected = new float[16]
            {
                12, 1.1f, 1, 0,
                10, 2.2f, -1, 0,
                8, -2.2f, 1.0f, -0,
                6, -1.1f, -1.0f, -0,
            };

            Assert.IsTrue(CheckFloatBytesEqual(expected, matrix.Values));
        }

        #endregion

        #region Utility Functions for Matrix Tests

        private bool CheckFloatBytesEqual(float[] expected, float[] actual)
        {
            if (expected.Length != actual.Length)
            {
                return false;
            }

            for (int i = 0; i < expected.Length; ++i)
            {
                if (expected[i] != actual[i])
                {
                    return false;
                }
            }

            return true;
        }

        #endregion
    }
}
