using DTO_Layer;
using System.Data;
using System.Data.SqlClient;

namespace DAL_Layer
{
    public class EventDAL : BaseDAL
    {
        public EventDAL() : base("Data Source=DELL-XPS-15;Initial Catalog=Eeventify;Integrated Security=True")
        {
        }

        public EventDTO? GetEvent(int Id)
        {
            string query = "SELECT * FROM dbo.Event WHERE ID=@Id";

            SqlParameter idParameter = new("@Id", SqlDbType.Int) { Value = Id };

            SqlCommand cmd = BaseDAL.CommandBuilder(query, idParameter);
            DataTable dataTable = base.RunQuery(cmd);

            if (dataTable.Rows.Count == 0)
            {
                return null;
            }
            return new EventDTO
            {
                ID = (int)dataTable.Rows[0]["Id"],
                Title = (string)dataTable.Rows[0]["Title"],
                LocationBased = (bool)dataTable.Rows[0]["LocationBased"],
                Latitude = (double)dataTable.Rows[0]["Latitude"],
                Longitude = (double)dataTable.Rows[0]["Longitude"],
                HostID = (int)dataTable.Rows[0]["HostID"],
                MaxPeople = (int)dataTable.Rows[0]["MaxPeople"],
                MinPeople = (int)dataTable.Rows[0]["MinPeople"],
                StartEvent = (DateTime)dataTable.Rows[0]["StartEvent"],
                HasStarted = (bool)dataTable.Rows[0]["HasStarted"]            
            };
        }

        //public EventDTO? GetUserByUsername(string username)
        //{
        //    string query = "SELECT * FROM dbo.Event WHERE Username=@Username";

        //    SqlParameter idParameter = new("@Username", SqlDbType.VarChar, 50) { Value = username };

        //    SqlCommand cmd = BaseDAL.CommandBuilder(query, idParameter);
        //    DataTable dataTable = base.RunQuery(cmd);

        //    if (dataTable.Rows.Count == 0)
        //    {
        //        return null;
        //    }
        //    return new EventDTO
        //    {
        //        ID = (int)dataTable.Rows[0]["Id"],
        //        Title = (string)dataTable.Rows[0]["Title"],
        //        LocationBased = (bool)dataTable.Rows[0]["LocationBased"],
        //        Latitude = (double)dataTable.Rows[0]["Latitude"],
        //        Longitude = (double)dataTable.Rows[0]["Longitude"],
        //        HostID = (int)dataTable.Rows[0]["HostID"],
        //        MaxPeople = (int)dataTable.Rows[0]["MaxPeople"],
        //        MinPeople = (int)dataTable.Rows[0]["MinPeople"],
        //        StartEvent = (DateTime)dataTable.Rows[0]["StartEvent"],
        //        HasStarted = (int)dataTable.Rows[0]["HasStarted"]
        //    };
        //}

        public bool AddUser(EventDTO eventDTO)
        {
            string query = "INSERT INTO dbo.Event VALUES (@Title, @Description, @IsLocationBased, @Latitude, @Longitude, @HostId, @MaxPeople, @MinPeople, @StartEvent, @HasStarted)";

            SqlParameter titleParam = new("@Title", SqlDbType.VarChar, 255) { Value = eventDTO.Title };
            SqlParameter descriptionParam = new("@Description", SqlDbType.VarChar, int.MaxValue) { Value = eventDTO.Description };
            SqlParameter locationParam = new("@IsLocationBased", SqlDbType.Bit) { Value = Convert.ToInt32(eventDTO.LocationBased) };
            SqlParameter latitudeParam = new("@Latitude", SqlDbType.Float) { Value = eventDTO.Latitude };
            SqlParameter longitudeParam = new("@Longitude", SqlDbType.Float) { Value = eventDTO.Longitude };
            SqlParameter hostidParam = new("@HostId", SqlDbType.Int) { Value = eventDTO.HostID };
            SqlParameter maxpeopleParam = new("@MaxPeople", SqlDbType.Int) { Value = eventDTO.MaxPeople };
            SqlParameter minpeopleParam = new("@MinPeople", SqlDbType.Int) { Value = eventDTO.MinPeople };
            SqlParameter starteventParam = new("@StartEvent", SqlDbType.DateTime) { Value = eventDTO.StartEvent };
            SqlParameter hasstartedParam = new("@HasStarted", SqlDbType.Bit) { Value = Convert.ToInt32(eventDTO.HasStarted) };
            SqlCommand cmd = BaseDAL.CommandBuilder(query, titleParam, descriptionParam, locationParam, latitudeParam, longitudeParam, hostidParam, maxpeopleParam, minpeopleParam, starteventParam, hasstartedParam);
            return base.RunNonQuery(cmd) == 1;
        }
    }
}