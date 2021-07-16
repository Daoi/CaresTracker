using System.Data;

namespace CaresTracker.DataAccess.DataAccessors.ResidentAccessors
{
    public class GetAllRaces : DataSupport, IData
    {
        /// <summary>
        /// Retrieve a DataTable containing all race drop down options.
        /// </summary>
        public GetAllRaces()
        {
            CommandText = "GetAllRaces";
            CommandType = CommandType.StoredProcedure;
        }

        public DataTable ExecuteCommand()
        {
            ExecuteQuery eq = new ExecuteQuery();
            return eq.ExecuteAdapter(this);
        }
    }
}