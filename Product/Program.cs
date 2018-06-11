using PracticeLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Product {
	class Program {
		static void Main(string[] args) {

			VendorController VendorCtrl = new VendorController(@"STUDENT02\SQLEXPRESS", "prssql");

			IEnumerable<Vendor> vendors = VendorCtrl.List();
			foreach (Vendor vendor1 in vendors) {
				Console.WriteLine($"{vendor1.Code} {vendor1.Name}");
			}

			Vendor vendor = VendorCtrl.Get(5);
			if (vendor == null) {
				Console.WriteLine("Vendor not found");
			} else {
				Console.WriteLine($"{vendor.Code} {vendor.Name}");
			}

			//Vendor newVendor = new Vendor();  //commenting out, already know this working and don't want to add more vendors
			//newVendor.Code = "TAR3";
			//newVendor.Name = "Target3";
			//newVendor.Address = "1234 Minnehaha Ave";
			//newVendor.City = "Minneapolis";
			//newVendor.State = "MN";
			//newVendor.Zip = "55417";
			//newVendor.Phone = "123-456-7890";
			//newVendor.Email = "spot@target.com";
			//newVendor.IsPreApproved = true;
			//newVendor.Active = true;
			//bool success = VendorCtrl.Create(newVendor);

			vendor = VendorCtrl.Get(9);
			vendor.Address = "5252 Drury Lane";
			bool success = VendorCtrl.Change(vendor);
			//VendorCtrl.CloseConnection();

			vendor = VendorCtrl.Get(8);
			success = VendorCtrl.Remove(vendor);
			VendorCtrl.CloseConnection();
		}

	}

}
