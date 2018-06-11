using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PracticeLibrary {
	public class VendorController {

		SqlConnection conn = null;
		SqlCommand cmd = new SqlCommand();

		private void SetupCommand(SqlConnection conn, string sql) {
			cmd.Connection = conn;
			cmd.CommandText = sql;
			cmd.Parameters.Clear();
		}

		public IEnumerable<Vendor> List() {
			string sql = "select * from Vendor";
			SetupCommand(conn, sql);
			SqlDataReader reader = cmd.ExecuteReader();
			List<Vendor> vendors = new List<Vendor>();
			//vendors.Add(new Vendor(reader));
			while (reader.Read()) {
				Vendor vendor = new Vendor(reader);
				vendors.Add(vendor);
			}
			reader.Close();
			return vendors;
		}

		public Vendor Get(int id) {
			string sql = "select * from Vendor where Id = @id";
			SetupCommand(conn, sql);
			cmd.Parameters.Add(new SqlParameter("@id", id));
			SqlDataReader reader = cmd.ExecuteReader();
			if (reader.HasRows == false) {
				reader.Close();
				return null;
			}
			reader.Read();
			Vendor vendor = new Vendor(reader);
			reader.Close();
			return vendor;
		}

			public bool Create(Vendor vendor) {
				string sql = "insert into Vendor" +
					" (Code, Name, Address, City, State, Zip, Phone, Email, IsPreApproved, Active) " +
					" VALUES " +
					" (@Code, @Name, @Address, @City, @State, @Zip, @Phone, @Email, @IsPreApproved, @Active) ";
				SetupCommand(conn, sql);
				cmd.Parameters.Add(new SqlParameter("@Code", vendor.Code));
				cmd.Parameters.Add(new SqlParameter("@Name", vendor.Name));
				cmd.Parameters.Add(new SqlParameter("@Address", vendor.Address));
				cmd.Parameters.Add(new SqlParameter("@City", vendor.City));
				cmd.Parameters.Add(new SqlParameter("@State", vendor.State));
				cmd.Parameters.Add(new SqlParameter("@Zip", vendor.Zip));
				cmd.Parameters.Add(new SqlParameter("@Phone", vendor.Phone));
				cmd.Parameters.Add(new SqlParameter("@Email", vendor.Email));
				cmd.Parameters.Add(new SqlParameter("@IsPreApproved", vendor.IsPreApproved));
				cmd.Parameters.Add(new SqlParameter("@Active", vendor.Active));

				int recsAffected = cmd.ExecuteNonQuery();   //this tells us how many rows are affected
				return (recsAffected == 1);
			}

		public bool Change(Vendor vendor) {
			string sql = "update Vendor set " +
						" Code = @Code, " +
						" Name = @Name, " +
						" Address = @Address, " +
						" City = @City, " +
						" State = @State, " +
						" Zip = @Zip, " +
						" Phone = @Phone, " +
						" Email = @Email, " +
						" IsPreApproved = @IsPreApproved, " +
						" Active = @Active " +
						" where Id = @Id"; 
			SetupCommand(conn, sql);
			cmd.Parameters.Add(new SqlParameter("@Id", vendor.Id));
			cmd.Parameters.Add(new SqlParameter("@Code", vendor.Code));
			cmd.Parameters.Add(new SqlParameter("@Name", vendor.Name));
			cmd.Parameters.Add(new SqlParameter("@Address", vendor.Address));
			cmd.Parameters.Add(new SqlParameter("@City", vendor.City));
			cmd.Parameters.Add(new SqlParameter("@State", vendor.State));
			cmd.Parameters.Add(new SqlParameter("@Zip", vendor.Zip));
			cmd.Parameters.Add(new SqlParameter("@Phone", vendor.Phone));
			cmd.Parameters.Add(new SqlParameter("@Email", vendor.Email));
			cmd.Parameters.Add(new SqlParameter("@IsPreApproved", vendor.IsPreApproved));
			cmd.Parameters.Add(new SqlParameter("@Active", vendor.Active));

			int recsAffected = cmd.ExecuteNonQuery();   //this tells us how many rows are affected
			return (recsAffected == 1);
		}

		public bool Remove(Vendor vendor) {
			string sql = "delete from vendor where id = @id;";
			SetupCommand(conn, sql);
			cmd.Parameters.Add(new SqlParameter("@id", vendor.Id));

			int recsAffected = cmd.ExecuteNonQuery();   //this tells us how many rows are affected
			return (recsAffected == 1);
		}

			private SqlConnection CreateAndOpenConnection(string server, string database) { //this is creating and opening our connection to the server
				string connStr = $"server={server};database={database};trusted_connection=true;"; //the STRING and STATEMENT require a semi colon
				SqlConnection conn = new SqlConnection(connStr);
				conn.Open();
				if (conn.State != System.Data.ConnectionState.Open) { //Always put in an exception to test if the connection opened
					throw new ApplicationException("Sql Connection did not open");
				}
				return conn;
			}

			public void CloseConnection() {
				if (conn != null && conn.State == System.Data.ConnectionState.Open) {
					conn.Close();
				}
			}

			public VendorController(string server, string database) {
				conn = CreateAndOpenConnection(server, database);
			}

			public VendorController() {

			}

		}
	}



