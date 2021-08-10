using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MovieHub
{
    public class Database
    {
        private SqlConnection myConnection = new SqlConnection();
        
        public bool RunningStatus { get; set; }


        public Database()
        {
            RunningStatus = true;
            // connect to the Movies Database
            string connectionString =
                @"Data Source=localhost\SQLEXPRESS;Initial Catalog=moviehub;Integrated Security=True;";
            if (!CheckDB(connectionString))
            {
                GenerateDB();
                RunningStatus = false;
             
            }
            else
            {
                myConnection.ConnectionString = connectionString;
                myConnection.Open();
            }
        }

        private bool CheckDB(string DBString)
        {
            SqlConnection con = new SqlConnection(DBString);
            try
            {
                con.Open();
                con.Close();
                return true;
            }
            catch
            {
                return false;
            }
        }

        private void GenerateDB()
        {
            SqlConnection cn;
            SqlCommand cm;
            try
            {
                string script = null;
                script = MovieHub.Properties.Resources.allscripts;
                string[] ScriptSplitter = script.Split(new string[] { "GO" }, StringSplitOptions.None);
                using (cn = new SqlConnection(@"Data Source=.\SQLEXPRESS;Initial Catalog=master;Integrated Security=True"))
                {
                    cn.Open();
                    foreach (string str in ScriptSplitter)
                    {
                        using (cm = cn.CreateCommand())
                        {
                            cm.CommandText = str;
                            cm.ExecuteNonQuery();
                        }
                    }
                }
                cn.Close();
            }
            catch (Exception ex)
            {

            }

        }

        /// <summary>
        /// REturn Connection State
        /// </summary>
        /// <returns></returns>
        public ConnectionState GetConnectionState()
        {
            return myConnection.State;
        }

        /// <summary>
        /// Function To Close The Connection
        /// </summary>
        public void CloseConnection()
        {
            // Check Connection is Opened or Not
            if (myConnection != null && myConnection.State == ConnectionState.Open)
            {
                myConnection.Close();
                myConnection.Dispose();
            }
        }

        /// <summary>
        /// Function to Execute Given Query and Return Result in Data Table
        /// </summary>
        /// <param name="sql">SQL Query</param>
        /// <returns>Data Table with Records</returns>
        public DataTable ExecuteQueryForDataTable(string sql)
        {
            // Create a data table
            DataTable dt = new DataTable();
            try
            {
                // Create an Command Object
                SqlCommand cmd = myConnection.CreateCommand();
                // Initialize Command Text
                cmd.CommandText = sql;
                // Prepare a Data Adapter
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                // Fill Data Table By Executing the Query with data
                adapter.Fill(dt);
            }
            catch (Exception ex)
            {
            }
            return dt;
        }


        /// <summary>
        /// SELECT * FROM Customers
        /// </summary>
        public DataTable FillCustomersDGV()
        {
            // Create a data table
            DataTable dt = ExecuteQueryForDataTable("SELECT * FROM Customers");
            return dt;
        }
        /// <summary>
        /// Add New Customer to DB
        /// </summary>
        public void AddNewCustomerToDB(string firstName, string lastName, string address, string phone)
        {
            try
            {
                // set the query to the SQL variable
                string SQL = "INSERT INTO Customers (FirstName, LastName, Address, Phone) VALUES (@First, @Last, @Address, @Phone)";
                SqlCommand myCommand = new SqlCommand(SQL, myConnection);
                // set the parameters
                myCommand.Parameters.AddWithValue("First", firstName);
                myCommand.Parameters.AddWithValue("Last", lastName);
                myCommand.Parameters.AddWithValue("Address", address);
                myCommand.Parameters.AddWithValue("Phone", phone);
                // run the query
                myCommand.ExecuteNonQuery();
                // alert user that the query was successful
                MessageBox.Show("The new customer has been added to the database.");
            }
            // alert the user if there was an error
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        /// <summary>
        /// Edit Customer in DB
        /// </summary>
        public void EditCustomerInDB(int customerID, string firstName, string lastName, string address, string phone)
        {
            // only run if there is something in the textboxes 
            if (firstName != "" && lastName != "" && address != "" && phone != "")
            {
                try
                {
                    // set the query to the SQL variable
                    string SQL = "UPDATE Customers SET FirstName = @First, LastName = @Last, Address = @Address, Phone = @Phone WHERE CustID = @Id";
                    SqlCommand myCommand = new SqlCommand(SQL, myConnection);
                    // set the parameters
                    myCommand.Parameters.AddWithValue("Id", customerID);
                    myCommand.Parameters.AddWithValue("First", firstName);
                    myCommand.Parameters.AddWithValue("Last", lastName);
                    myCommand.Parameters.AddWithValue("Address", address);
                    myCommand.Parameters.AddWithValue("Phone", phone);
                    // run the query
                    myCommand.ExecuteNonQuery();

                    // alert user that the query was successful
                    MessageBox.Show("The customer details have been edited in the database.");
                }
                // alert the user if there was an error
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                // prompt user to complete all text fields
                MessageBox.Show("Please complete all textboxes.");
            }
        }
        /// <summary>
        /// Delete the selected customer
        /// </summary>
        public void DeleteCustomer(int CustomerId)
        {
            try
            {
                // set the query to the SQL variable
                string SQL = "DELETE FROM Customers WHERE CustID = @Id";
                SqlCommand myCommand = new SqlCommand(SQL, myConnection);
                // set the parameters
                myCommand.Parameters.AddWithValue("Id", CustomerId);
                // run the query
                myCommand.ExecuteNonQuery();
                // alert user that the query was successful
                MessageBox.Show("This customer has been deleted from the database.");
            }
            // alert the user if there was an error
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        /// <summary>
        /// SELECT * FROM Movies
        /// </summary>
        public DataTable FillMoviesDGV()
        {
            // Create a data table
            DataTable dt = ExecuteQueryForDataTable("SELECT * FROM Movies");
            return dt;
        }
        /// <summary>
        /// Add new movie to DB
        /// </summary>
        public void AddNewMovieToDB(string rating, string title, string year, string cost, string copies, string plot, string genre)
        {
            try
            {
                // set the query to the SQL variable
                string SQL = "INSERT INTO Movies (Rating, Title, Year, Rental_Cost, Copies, Plot, Genre) VALUES (@Rating, @Title, @Year, @Cost, @Copies, @Plot, @Genre)";
                SqlCommand myCommand = new SqlCommand(SQL, myConnection);
                // set the parameters
                myCommand.Parameters.AddWithValue("Rating", rating);
                myCommand.Parameters.AddWithValue("Title", title);
                myCommand.Parameters.AddWithValue("Year", year);
                myCommand.Parameters.AddWithValue("Cost", cost);
                myCommand.Parameters.AddWithValue("Copies", copies);
                myCommand.Parameters.AddWithValue("Plot", plot);
                myCommand.Parameters.AddWithValue("Genre", genre);
                // run the query
                myCommand.ExecuteNonQuery();
                // alert user that the query was successful
                MessageBox.Show("The new movie has been added to the database.");
            }
            // alert the user if there was an error
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        /// <summary>
        /// Edit movie in DB
        /// </summary>
        public void EditMovieInDB(int movieId, string rating, string title, string year, string cost, string copies, string plot, string genre)
        {
            try
            {
                // set the query to the SQL variable
                string SQL = "UPDATE Movies SET Rating = @Rating, Title = @Title, Year = @Year, Rental_Cost = @Cost, Copies = @Copies, Plot = @Plot, Genre = @Genre WHERE MovieID = @Id";

                SqlCommand myCommand = new SqlCommand(SQL, myConnection);
                // set the parameters
                myCommand.Parameters.AddWithValue("Id", movieId);
                myCommand.Parameters.AddWithValue("Rating", rating);
                myCommand.Parameters.AddWithValue("Title", title);
                myCommand.Parameters.AddWithValue("Year", year);
                myCommand.Parameters.AddWithValue("Cost", cost);
                myCommand.Parameters.AddWithValue("Copies", copies);
                myCommand.Parameters.AddWithValue("Plot", plot);
                myCommand.Parameters.AddWithValue("Genre", genre);
                // run the query
                myCommand.ExecuteNonQuery();
                // alert user that the query was successful
                MessageBox.Show("The movie details have been updated in the database.");
            }
            // alert the user if there was an error
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        /// <summary>
        /// Delete selected movie from the DB
        /// </summary>
        public void DeleteMovie(int movieId)
        {
            try
            {
                // set the query to the SQL variable
                string SQL = "DELETE FROM Movies WHERE MovieID = @Id";

                SqlCommand myCommand = new SqlCommand(SQL, myConnection);
                // set the parameters
                myCommand.Parameters.AddWithValue("Id", movieId);
                // run the query
                myCommand.ExecuteNonQuery();
                // alert user that the query was successful
                MessageBox.Show("This movie has been deleted from the database.");
            }
            // alert the user if there was an error
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        /// <summary>
        /// SELECT * FROM Rentals
        /// </summary>
        public DataTable FillRentalsDGV()
        {
            // Create a data table
            DataTable dt = ExecuteQueryForDataTable("SELECT * FROM ShowRentals");
            return dt;
        }
        /// <summary>
        /// SELECT * FROM RentalsOutNow
        /// </summary>
        public DataTable ShowRentedOutMovies()
        {
            // Create a data table
            DataTable dt = ExecuteQueryForDataTable("SELECT * FROM ShowRentalsOutNow");
            return dt;
        }
        /// <summary>
        /// Update selected movie with DateReturned
        /// </summary>
        public void ReturnMovie(int rentalId)
        {
            string currentDate = DateTime.Now.ToString();
            DateTime date = Convert.ToDateTime(currentDate);
            try
            {
                // set the query to the SQL variable
                string SQL = "UPDATE RentedMovies SET DateReturned = @Date WHERE RMID = @Id";
                SqlCommand myCommand = new SqlCommand(SQL, myConnection);
                // set the parameters
                myCommand.Parameters.AddWithValue("Id", rentalId);
                myCommand.Parameters.AddWithValue("Date", date);
                // run the query
                myCommand.ExecuteNonQuery();
                // alert user that the query was successful
                MessageBox.Show("This movie has been successfully returned.");
            }
            // alert the user if there was an error
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        /// <summary>
        /// Rent out a movie
        /// </summary>
        public void RentOutMovie(int customer, int movie, DateTime date)
        {
            try
            {
                // set the query to the SQL variable
                string SQL = "INSERT INTO RentedMovies (MovieIDFK, CustIDFK, DateRented) VALUES (@Movie, @Customer, @Date)";
                SqlCommand myCommand = new SqlCommand(SQL, myConnection);
                // set the parameters
                myCommand.Parameters.AddWithValue("Customer", customer);
                myCommand.Parameters.AddWithValue("Movie", movie);
                myCommand.Parameters.AddWithValue("Date", date);
                // run the query
                myCommand.ExecuteNonQuery();

                // alert user that the query was successful
                MessageBox.Show("The movie has been issued successfully.");
            }
            // alert the user if there was an error
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        /// <summary>
        /// Search for customers first name
        /// </summary>
        public DataTable SearchCustomers(string search)
        {
            DataTable dt = new DataTable();
            string SQL = "SELECT * FROM Customers WHERE FirstName LIKE @SearchName";
            SqlDataAdapter da = new SqlDataAdapter(SQL, myConnection);
            // set the parameters
            da.SelectCommand.Parameters.AddWithValue("@SearchName", "%" + search + "%");
            // fill the datatable with results from the query
            da.Fill(dt);
            // pass the datatable data to the DGV
            return dt;
        }
        /// <summary>
        /// SELECT * FROM ShowCustomers
        /// </summary>
        public DataTable ShowTopCustomers()
        {
            // Create a data table
            DataTable dt = new DataTable();
            // Create a Sql Command
            SqlCommand cmd = myConnection.CreateCommand();
            // Set Command Type of Stored Procedure
            cmd.CommandType = CommandType.StoredProcedure;
            // Set the Procedure Name
            cmd.CommandText = "ShowCustomers";
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            // fill the datatable with the data from the SQL
            da.Fill(dt);
            // pass the datatable data to the DGV
            return dt;
        }
        /// <summary>
        /// SELECT * FROM ShowMovies
        /// </summary>
        public DataTable ShowTopMovies()
        {
            // Create a data table
            DataTable dt = new DataTable();
            // Create a Sql Command
            SqlCommand cmd = myConnection.CreateCommand();
            // Set Command Type of Stored Procedure
            cmd.CommandType = CommandType.StoredProcedure;
            // Set the Procedure Name
            cmd.CommandText = "ShowMovies";
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            // fill the datatable with the data from the SQL
            da.Fill(dt);
            // pass the datatable data to the DGV
            return dt;
        }
        /// <summary>
        /// Search for movie title
        /// </summary>
        public DataTable SearchMovies(string search)
        {
            DataTable dt = new DataTable();
            // set the query to the SQL variable
            string SQL = "SELECT * FROM Movies WHERE Title LIKE @SearchName";
            SqlDataAdapter da = new SqlDataAdapter(SQL, myConnection);
            // set the parameters
            da.SelectCommand.Parameters.AddWithValue("@SearchName", "%" + search + "%");
            da.Fill(dt);
            // pass the datatable data to the DGV
            return dt;
        }
        /// <summary>
        /// Count how many copies of the movie are rented out currently
        /// </summary>
        public int CheckCopiesOut(int MID)
        {
            // set the query to the SQL variable
            string SQL = "SELECT Count(*) FROM RentedMovies WHERE MovieIDFK = @MID AND DateReturned IS NULL";
            SqlCommand myCommand = new SqlCommand(SQL, myConnection);
            // set the parameters
            myCommand.Parameters.AddWithValue("MID", MID);                
            // run the query
            int result = Convert.ToInt16(myCommand.ExecuteScalar());
            return result;
        }
    }
}
