using System.Diagnostics;

namespace OpenMaths
{
    /// <summary>
    /// A row major 4x4 float matrix class for use with SharpGL
    /// </summary>
    public class Matrix
    {
        #region Properties and Fields

        /// <summary>
        /// A 16 length array of floats representing the rows of the matrix one after the other.
        /// Row 1 is in the range [0,3].
        /// Row 2 is in the range [4,7].
        /// Row 3 is in the range [8,11].
        /// Row 4 is in the range [12,15].
        /// </summary>
        internal float[] Values { get; private set; }

        /// <summary>
        /// The 4x4 identity matrix.
        /// </summary>
        public static Matrix Identity = new Matrix(new float[16]
        {
            1, 0, 0, 0,
            0, 1, 0, 0,
            0, 0, 1, 0,
            0, 0, 0, 1,
        });

        #endregion

        // Currently you can change the Indentity matrix - this is not good

        /// <summary>
        /// Default constructor which will create a 4x4 identity matrix.
        /// </summary>
        public Matrix()
        {
            Values = new float[16]
            {
                1, 0, 0, 0,
                0, 1, 0, 0,
                0, 0, 1, 0,
                0, 0, 0, 1,
            };
        }

        /// <summary>
        /// Constructor which takes a 16 length array of floats to populate the matrix with.
        /// </summary>
        /// <param name="values">The rows of the matrix concatenated together into a single array.</param>
        public Matrix(float[] values)
        {
            Debug.Assert(values.Length == 16, "16 values not inputted to matrix constructor");
            Values = values;
        }

        /// <summary>
        /// Constructor which copies the values of the inputted matrix into the newly constructed matrix object.
        /// </summary>
        /// <param name="other"></param>
        public Matrix(Matrix other)
        {
            Values = other.Values;
        }

        #region Multiplication

        /// <summary>
        /// Overridden multiply operator.
        /// LHS is multiplied by RHS and a fresh matrix with the new values is returned.
        /// Example usage:
        /// Matrix result = A * B;
        /// Will return the matrix representing the linear map B(A(x)), i.e. A performed first then B.
        /// </summary>
        /// <param name="lhs"></param>
        /// <param name="rhs"></param>
        /// <returns>A new matrix containing the multiplied values</returns>
        public static Matrix operator *(Matrix lhs, Matrix rhs)
        {
            Matrix result = new Matrix(lhs);
            result.Multiply(rhs);

            return result;
        }

        /// <summary>
        /// Multiplies the inputted matrices together as lhs * rhs with a fresh matrix with the new values being returned.
        /// Example usage:
        /// Matrix result = Matrix.Multiply(A, B).
        /// Will return the matrix representing the linear map B(A(x)), i.e. A performed first then B.
        /// </summary>
        /// <param name="lhs"></param>
        /// <param name="rhs"></param>
        /// <returns></returns>
        public static Matrix Multiply(Matrix lhs, Matrix rhs)
        {
            return lhs * rhs;
        }

        /// <summary>
        /// Multiplies this matrix by the inputted matrix and alters the values stored in this matrix.
        /// Example usage:
        /// A.Multiply(B).
        /// A will now contain the matrix representing the linear map B(A(x)), i.e. A performed first then B.
        /// </summary>
        /// <param name="rhs"></param>
        public void Multiply(Matrix rhs)
        {
            float[] temp = new float[4];

            for (int i = 0; i < 4; ++i)
            {
                for (int j = 0; j < 4; ++j)
                {
                    for (int k = 0; k < 4; ++k)
                    {
                        temp[j] = rhs.Values[i * 4 + k] * Values[k * 4 + j];
                    }
                }

                for (int index = 0; index < 4; ++index)
                {
                    Values[i * 4 + index] = temp[index];
                }
            }
        }

        #endregion

        #region Equality

        /// <summary>
        /// Checks to see if the values of the lhs matrix are the same as the values of the rhs matrix.
        /// </summary>
        /// <param name="lhs"></param>
        /// <param name="rhs"></param>
        /// <returns><c:false>If a value in a position of the lhs matrix is not the same as the value in the same position as the rhs matrix.</false><c:true>Otherwise.</true></returns>
        public static bool operator ==(Matrix lhs, Matrix rhs)
        {
            for (int i = 0; i < lhs.Values.Length; ++i)
            {
                if (lhs.Values[i] != rhs.Values[i])
                {
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// Checks to see if the values of the lhs matrix are different to the values of the rhs matrix.
        /// </summary>
        /// <param name="lhs"></param>
        /// <param name="rhs"></param>
        /// <returns><c:true>If a value in a position of the lhs matrix is not the same as the value in the same position as the rhs matrix.</true><c:false>Otherwise.</false></returns>
        public static bool operator !=(Matrix lhs, Matrix rhs)
        {
            for (int i = 0; i < lhs.Values.Length; ++i)
            {
                if (lhs.Values[i] == rhs.Values[i])
                {
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// Checks to see whether the values of the inputted matrix are the same as the values of this.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns><c:false>If a value in a position of this matrix is not the same as the value in the same position as the inputted matrix.</false><c:true>Otherwise.</true></returns>
        public override bool Equals(object obj)
        {
            Debug.Assert(obj is Matrix, "Inputted object is not a Matrix.");
            return this == (Matrix)obj;
        }

        /// <summary>
        /// An initial test to determine the equality of two matrices.
        /// </summary>
        /// <returns>Sums the values of this matrix and then returns the sum truncated to an int;</returns>
        public override int GetHashCode()
        {
            float hash = 0;
            foreach (float value in Values)
            {
                hash += value;
            }

            return (int)hash;
        }

        #endregion

        #region Explicit Casts

        /// <summary>
        /// Explicit cast operator to retrieve the matrix as a float array.
        /// </summary>
        /// <param name="matrix"></param>
        public static explicit operator float[](Matrix matrix)
        {
            // This exoses the underlying values
            // Is this legit?
            // Can we alter the casted array and have it feed back to this?
            // Test this
            return matrix.Values;
        }

        /// <summary>
        /// Return the underlying values of this matrix in a float array.
        /// Consider using (float[] instead).
        /// </summary>
        /// <returns></returns>
        public float[] ToFloatArray()
        {
            return Values;
        }

        #endregion

        #region Utility Functions

        /// <summary>
        /// Returns a matrix whose values are the transpose of the inputted matrix.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static Matrix Transpose(Matrix input)
        {
            Matrix result = new Matrix(input);
            result.Transpose();

            return result;
        }

        /// <summary>
        /// Transposes this matrix object.
        /// </summary>
        public void Transpose()
        {
            float temp = 0;

            for (int row = 1; row < 4; ++row)
            {
                for (int column = row; column < 4; ++column)
                {
                    // The value we want to swap is always shifted along by four in our array
                    temp = Values[(row - 1) * 4 + column];
                    Values[(row - 1) * 4 + column] = Values[(row) * 4 + column];
                    Values[(row) * 4 + column] = temp;
                }
            }
        }

        #endregion

        #region Rendering Functions

        // Create look at, create projection etc.

        #endregion
    }
}