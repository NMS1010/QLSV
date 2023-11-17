//using QLSV.Interfaces;
//using QLSV.InterfacesService;
//using QLSV.Models;
//using QLSV.OtpModels;
//using System.Collections.Generic;
//using System.Linq;

//namespace QLSV.Services
//{
//    public class DiemHocSinhService : IDiemHocSinhService
//    {
//        private readonly IDiemHocSinhRepository _DiemHocSinhRepository;
//        private readonly IProductRepository _productRepository;
//        public DiemHocSinhService(IDiemHocSinhRepository DiemHocSinhRepository,IProductRepository productRepository)
//        {
//            _DiemHocSinhRepository = DiemHocSinhRepository;
//            _productRepository = productRepository;
//        }

//        public List<DiemHocSinhDetail> getDiemHocSinh(int id)
//        {
//            var products = _productRepository.GetAll();
//            var libraries = _DiemHocSinhRepository.GetAll();
//            var detail = from p in products
//                         join lib in libraries
//                         on p.Id equals lib.IdKhoaHoc
//                         where lib.IdHocSinh == id
//                         select new DiemHocSinhDetail
//                         {
//                             UserID=lib.UserId,
//                             Id=p.Id,
//                             Name=p.Name,
//                             Image=p.Image
//                         };
//            return detail.ToList();
//        }

//        public DiemHocSinh remove(int userID, int productID)
//        {
//            var getDiemHocSinh = _DiemHocSinhRepository.GetAll().Where(t => (t.ProductId == productID) && (t.UserId == userID)).Single();
//            return getDiemHocSinh;
//        }

//        public List<DiemHocSinh> updateDiemHocSinh(int userID, List<Cart> cart)
//        {
//            List<DiemHocSinh> libraries = new List<DiemHocSinh>();
//            foreach (var p in cart)
//            {
//                DiemHocSinh DiemHocSinh = new DiemHocSinh()
//                {
//                    UserId = userID,
//                    ProductId = p.product.Id
//                };
//                libraries.Add(DiemHocSinh);
//            }
//            return libraries;
//        }
//    }
//}
