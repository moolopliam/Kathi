using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace concert.Models
{

    public partial class VTitle
    {

        [Display(Name = "รหัสคำนำหน้าชื่อ")]
        public int IDTitle { get; set; }

        [Display(Name = "ชื่อคำนำหน้า")]
        [Required(ErrorMessage = "กรุณาป้อนข้อมูล")]
        public string NameTi { get; set; }
    }
    [MetadataType(typeof(VTitle))]
    public partial class Title { }
    ///////////////////////////////////////
    ///


    public partial class VBand
    {
        [Display(Name = "รหัสวง")]
        public int IDBand { get; set; }

        [Display(Name = "ชื่อวงดนตรี")]
        [Required(ErrorMessage = "กรุณาป้อนข้อมูล")]
        public string NameBand { get; set; }

        [Display(Name = "รหัสประเภทวงดนตรี")]
        [Required(ErrorMessage = "กรุณาป้อนข้อมูล")]
        public Nullable<int> IDCategory { get; set; }

    }
    [MetadataType(typeof(VBand))]
    public partial class Band { }
    ///////////////////////////////////////
    ///

    public partial class VCategory
    {
        [Display(Name = "รหัสประเภทวงดนตรี")]
        public int IDCategory { get; set; }

        [Display(Name = "ชื่อประเภทวงดนตรี")]
        [Required(ErrorMessage = "กรุณาป้อนข้อมูล")]
        public string NameCatgory { get; set; }
    }
    [MetadataType(typeof(VCategory))]
    public partial class Category { }
    ///////////////////////////////////////
    ///

    public partial class VDistrict
    {
        [Display(Name = "รหัสอำเภอ")]
        public int IDDistrict { get; set; }

        [Display(Name = "ชื่ออำเภอ")]
        [Required(ErrorMessage = "กรุณาป้อนข้อมูล")]
        public string NameDistrict { get; set; }

        [Display(Name = "รหัสจังหวัด")]
        public Nullable<int> IDProvince { get; set; }
    }
    [MetadataType(typeof(VDistrict))]
    public partial class District { }
    ///////////////////////////////////////
    ///


    public partial class VProvince
    {
        [Display(Name = "รหัสจังหวัด")]
        public int IDProvince { get; set; }

        [Display(Name = "ชื่อจังหวัด")]
        [Required(ErrorMessage = "กรุณาป้อนข้อมูล")]
        public string NameProvince { get; set; }

    }
    [MetadataType(typeof(VProvince))]
    public partial class Province { }
    ///////////////////////////////////////
    ///

    public partial class VPlace
    {
        [Display(Name = "รหัสสถานที่")]
        public int IDPlace { get; set; }

        [Display(Name = "ชื่อสถานที่")]
        [Required(ErrorMessage = "กรุณาป้อนข้อมูล")]
        public string NamePlace { get; set; }

        [Display(Name = "รหัสอำเภอ")]
        [Required(ErrorMessage = "กรุณาป้อนข้อมูล")]
        public Nullable<int> IDDistrict { get; set; }

    }
    [MetadataType(typeof(VPlace))]
    public partial class Place { }
    ///////////////////////////////////////
    ///

    public partial class VZone
    {
        [Display(Name = "รหัสโซน")]
        public int IDZone { get; set; }

        [Display(Name = "ชื่อโซน")]
        [Required(ErrorMessage = "กรุณาป้อนข้อมูล")]
        public string NameZone { get; set; }
    }
    [MetadataType(typeof(VZone))]
    public partial class Zone { }
    ///////////////////////////////////////
    ///

    public partial class VCustomer
    {
        [Display(Name = "รหัสลูกค้า")]
        public int IDCus { get; set; }

        [Display(Name = "ชื่อ-สกุล")]
        [Required(ErrorMessage = "กรุณาป้อนข้อมูล")]
        public string NameCus { get; set; }

        [Display(Name = "คำนำหน้าชื่อ")]
        [Required(ErrorMessage = "กรุณาป้อนข้อมูล")]
        public Nullable<int> IDTitle { get; set; }

        [Display(Name = "เบอร์โทรศัพท์")]
        [Required(ErrorMessage = "กรุณาป้อนข้อมูล")]
        public string Tel { get; set; }

        [Display(Name = "เลขบัตรประชาชน")]
        [Required(ErrorMessage = "กรุณาป้อนข้อมูล")]
        public string Card { get; set; }

    }
    [MetadataType(typeof(VCustomer))]
    public partial class Customer { }
    /////////////////////////////////////

    public partial class VBooking
    {
        [Display(Name = "รหัสการจอง")]
        public int IDBooking { get; set; }

        public Nullable<int> B_OrderID { get; set; }

        [Display(Name = "รหัสการโชว์")]
        public Nullable<int> IDShow { get; set; }

        [Display(Name = "รหัสลูกค้า")]      
        public Nullable<int> IDCus { get; set; }

        [Display(Name = "จำกัดบัตร")]
        [Required(ErrorMessage = "กรุณาป้อนข้อมูล")]
        public Nullable<int> NumCard { get; set; }

        [Display(Name = "วันที่จอง")]
        [Required(ErrorMessage = "กรุณาป้อนข้อมูล")]
        public string DateBooking { get; set; }

        [Display(Name = "โซน")]
        public virtual Zone Zone { get; set; }

        [Display(Name = "ราคารวม")]
        public Nullable<int> Total { get; set; }
    }
    [MetadataType(typeof(VBooking))]
    public partial class Booking { }
    /////////////////////////////////////
    ///


    public partial class VShow
    {
        [Display(Name = "รหัสการแสดง")]
        public int IDShow { get; set; }

        [Display(Name = "ชื่อการแสดง")]
        [Required(ErrorMessage = "กรุณาป้อนข้อมูล")]
        public string NameShow { get; set; }

        [Display(Name = "รหัสวงที่ทำการแสดง")]
        public Nullable<int> IDBand { get; set; }

        [Display(Name = "รหัสสถานที่")]
        public Nullable<int> IDPlace { get; set; }

        [Display(Name = "วันที่แสดง")]
        public string Date { get; set; }

        //[Display(Name = "เวลาที่ทำการแสดง")]
        //[Required(ErrorMessage = "กรุณาป้อนข้อมูล")]
        //public string Time { get; set; }

        [Display(Name = "จำนวนบัตรสูงสุด")]
        [Required(ErrorMessage = "กรุณาป้อนข้อมูล")]
        public string MatTicket { get; set; }

        [Display(Name = "รูปภาพ")]
        public byte[] Picture { get; set; }
    }
    /////////////////////////////////////
    ///
    [MetadataType(typeof(VShow))]
    public partial class Show { }
}