using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoApp.DataAccess.Inerface;
using ToDoApp.DataAccess.Model;

namespace ToDoApp.DataAccess.Respository
{
    public class ToDoRepository : IToDoRepository
    {
        #region "Variables Declaration"
        private readonly string mConnectionString;
        private SqlConnection mConnection;
        #endregion

        #region "Constructor"
        public ToDoRepository(IConfiguration configuration)
        {
            mConnectionString = configuration.GetConnectionString("ToDoConnection");
        }

        #endregion
        public async Task<List<ToDo>> GetToDoList()
        {
            DataSet DS = new DataSet();
            SqlCommand sqlCommand = null;
            SqlDataAdapter DA = null;
            List<ToDo> toDoColl = null;
            try
            {
                mConnection = new SqlConnection(mConnectionString);

                sqlCommand = new SqlCommand("spGetToDoList", mConnection);

                sqlCommand.CommandType = CommandType.StoredProcedure;

                mConnection.Open();
               

                DA = new SqlDataAdapter(sqlCommand);
                DA.Fill(DS);

                if (DS != null)
                {

                    if (DS.Tables.Count > 0)
                    {
                        toDoColl = new List<ToDo>();

                        foreach (DataRow dr in DS.Tables[0].Rows)
                        {
                            ToDo toDo = new ToDo();
                            toDo.ToDoId = Convert.ToInt32(dr["todo_id"]);
                            toDo.Title = Convert.ToString(dr["title"]);
                            toDo.IsCompleted = Convert.ToBoolean(dr["is_completed"]);
                            toDo.CreatedDate = Convert.ToDateTime(dr["created_date"]);
                            toDo.ModifiedDate = (dr["modified_date"]!=DBNull.Value)? Convert.ToDateTime(dr["modified_date"]): null;

                            toDoColl.Add(toDo);

                            toDo = null;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (mConnection != null)
                {
                    if (mConnection.State != ConnectionState.Closed)
                    {
                        mConnection.Close();
                    }
                    mConnection.Dispose();
                    mConnection = null;
                    sqlCommand = null;
                }
            }
            return toDoColl;
        }

        public async Task<ToDo> GetToDoDetailById(int toDoId)
        {
            DataSet DS = new DataSet();
            SqlCommand sqlCommand = null;
            SqlDataAdapter DA = null;
            ToDo toDo = null;
            try
            {
                mConnection = new SqlConnection(mConnectionString);

                sqlCommand = new SqlCommand("spGetToDoDetailByID", mConnection);

                sqlCommand.CommandType = CommandType.StoredProcedure;

                sqlCommand.Parameters.Add("@todo_id", SqlDbType.Int).Value = toDoId;

                mConnection.Open();

                DA = new SqlDataAdapter(sqlCommand);
                DA.Fill(DS);

                if (DS != null)
                {

                    if (DS.Tables.Count > 0)
                    {
                        DataRow dr = DS.Tables[0].Rows[0];

                        toDo = new ToDo();
                        toDo.ToDoId = Convert.ToInt32(dr["todo_id"]);
                        toDo.Title = Convert.ToString(dr["title"]);
                        toDo.IsCompleted = Convert.ToBoolean(dr["is_completed"]);
                        toDo.CreatedDate = Convert.ToDateTime(dr["created_date"]);
                        toDo.ModifiedDate = (dr["modified_date"] != DBNull.Value) ? Convert.ToDateTime(dr["modified_date"]) : null;
                    }

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (mConnection != null)
                {
                    if (mConnection.State != ConnectionState.Closed)
                    {
                        mConnection.Close();
                    }
                    mConnection.Dispose();
                    mConnection = null;
                    sqlCommand = null;
                }
            }
            return toDo;
        }

        public async Task<bool> SaveToDo(ToDo toDoToSave, string mode)
        {
            bool result = false;
            SqlCommand sqlCommand = null;
            try
            {
                mConnection = new SqlConnection(mConnectionString);

                sqlCommand = new SqlCommand("spSaveToDo", mConnection);

                sqlCommand.CommandType = CommandType.StoredProcedure;

                if(mode == "U")
                    sqlCommand.Parameters.Add("@todo_id", SqlDbType.Int).Value = toDoToSave.ToDoId;
                sqlCommand.Parameters.Add("@title", SqlDbType.VarChar).Value = toDoToSave.Title;
                sqlCommand.Parameters.Add("@is_completed", SqlDbType.Bit).Value = toDoToSave.IsCompleted;
                sqlCommand.Parameters.Add("@mode", SqlDbType.VarChar).Value = mode;

                mConnection.Open();

                sqlCommand.ExecuteNonQuery();

                result = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (mConnection != null)
                {
                    if (mConnection.State != ConnectionState.Closed)
                    {
                        mConnection.Close();
                    }
                    mConnection.Dispose();
                    mConnection = null;
                    sqlCommand = null;
                }
            }
            return result;
        }

        public async Task<bool> DeleteToDo(int toDoId)
        {
            bool result = false;
            SqlCommand sqlCommand = null;
            try
            {
                mConnection = new SqlConnection(mConnectionString);

                sqlCommand = new SqlCommand("spDeleteToDo", mConnection);

                sqlCommand.CommandType = CommandType.StoredProcedure;

                sqlCommand.Parameters.Add("@todo_id", SqlDbType.Int).Value = toDoId;

                mConnection.Open();

                sqlCommand.ExecuteNonQuery();

                result = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (mConnection != null)
                {
                    if (mConnection.State != ConnectionState.Closed)
                    {
                        mConnection.Close();
                    }
                    mConnection.Dispose();
                    mConnection = null;
                    sqlCommand = null;
                }
            }
            return result;
        }

        public async Task<bool> UpdateToDoCompletedStatus(int toDoId, bool isCompleted)
        {
            bool result = false;
            SqlCommand sqlCommand = null;
            try
            {
                mConnection = new SqlConnection(mConnectionString);

                sqlCommand = new SqlCommand("spUpdateToDoCompletedStatus", mConnection);

                sqlCommand.CommandType = CommandType.StoredProcedure;

                sqlCommand.Parameters.Add("@todo_id", SqlDbType.Int).Value = toDoId;
                sqlCommand.Parameters.Add("@is_completed", SqlDbType.Bit).Value = isCompleted;

                mConnection.Open();

                sqlCommand.ExecuteNonQuery();

                result = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (mConnection != null)
                {
                    if (mConnection.State != ConnectionState.Closed)
                    {
                        mConnection.Close();
                    }
                    mConnection.Dispose();
                    mConnection = null;
                    sqlCommand = null;
                }
            }
            return result;
        }
    }
}
