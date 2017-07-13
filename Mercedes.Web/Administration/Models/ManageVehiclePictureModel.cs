using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Mercedes.Admin.Models
{
    public class ManageVehiclePictureModel
    {
        public ManageVehiclePictureModel()
        {
            VehiclePictures = new List<VehiclePictureModel>();
            NewPictureModel = new VehiclePictureModel();
        }
        public int VehicleModelId { get; set; }
        public List<VehiclePictureModel> VehiclePictures { get; set; }
        public VehiclePictureModel NewPictureModel { get; set; }
    }
}