using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KavirTire.Shop.Infrastructure.SyncService.CRM.Models.Contact;
using KavirTire.Shop.Infrastructure.SyncService.CRM.Repository;
using KavirTire.Shop.Infrastructure.SyncService.Shop.Models;
using KavirTire.Shop.Infrastructure.SyncService.Shop.Persistence;
using NLog;
using GeneralPolicy = KavirTire.Shop.Infrastructure.SyncService.Shop.Models.GeneralPolicy;
using InventoryItem = KavirTire.Shop.Infrastructure.SyncService.Shop.Models.InventoryItem.InventoryItem;
using Location = KavirTire.Shop.Infrastructure.SyncService.Shop.Models.Location;
using PostCostCategory = KavirTire.Shop.Infrastructure.SyncService.Shop.Models.PostCostCategory;
using PriceListItem = KavirTire.Shop.Infrastructure.SyncService.Shop.Models.PriceListItem;
using Product = KavirTire.Shop.Infrastructure.SyncService.Shop.Models.Product;
using ProductImage = KavirTire.Shop.Infrastructure.SyncService.Shop.Models.ProductImage;
using Vehicle = KavirTire.Shop.Infrastructure.SyncService.Shop.Models.Vehicle;
using VehicleType = KavirTire.Shop.Infrastructure.SyncService.Shop.Models.VehicleType;
using VehicleTypeProduct = KavirTire.Shop.Infrastructure.SyncService.Shop.Models.VehicleTypeProduct;
using WebPage = KavirTire.Shop.Infrastructure.SyncService.Shop.Models.WebPage;

namespace KavirTire.Shop.Infrastructure.SyncService.Services
{
    public class DataMigrationService
    {
        private readonly Logger logger = LogManager.GetCurrentClassLogger();
        private readonly IShopRepository<VehicleType> _vehicleTypeShopRepo;
        private readonly VehicleTypeCrmRepository _vehicleTypeCrmRepo;

        private readonly IShopRepository<PriceList> _priceListShopRepo;
        private readonly IShopRepository<PriceListItem> _priceListItemShopRepo;
        private readonly PriceListCrmRepository _priceListCrmRepo;

        private readonly IShopRepository<GeneralPolicy> _generalPolicyShopRepo;
        private readonly GeneralPolicyCrmRepository _generalPolicyCrmRepo;

        private readonly IShopRepository<Location> _locationShopRepo;
        private readonly LocationCrmRepository _locationCrmRepo;


        private readonly ProductCrmRepository _productCrmRepo;
        private readonly IShopRepository<WebFile> _webFileShopRepo;
        private readonly IShopRepository<Product> _productRepo;
        private readonly IShopRepository<ProductImage> _productImageRepo;
        private readonly IShopRepository<VehicleTypeProduct> _vehicleTypeProductRepo;
        private readonly IShopRepository<InventoryItem> _inventoryItemsRepo;


        private readonly PostCostCategoryCrmRepository _postCostCategoryCrmRepo;
        private readonly IShopRepository<PostCostCategory> _postCostCategoryShopRepo;

        private readonly ContactCrmRepository _contactCrmRepo;
        private readonly IShopRepository<Customer> _customerRepo;

        private readonly VehicleCrmRepository _vehicleCrmRepo;
        private readonly IShopRepository<Vehicle> _vehicleShopRepo;

        private readonly IShopRepository<OrderHistory> _orderHistoryRepo;
        private readonly OrderCrmRepository _orderCrmRepo;

        private readonly IShopRepository<WebPage> _webPageShopRepo;
        private readonly WebPageCrmRepository _webPageCrmRepo;


        private readonly IpgCrmRepository _ipgCrmRepo;
        private readonly IShopRepository<Ipg> _ipgShopRepo;
        private readonly IShopRepository<BankAccount> _bankAccountShopRepo;

        public DataMigrationService(IShopRepository<VehicleType> vehicleTypeShopRepo,
            VehicleTypeCrmRepository vehicleTypeCrmRepo,
            IShopRepository<PriceList> priceListShopRepo,
            PriceListCrmRepository priceListCrmRepo,
            IShopRepository<PriceListItem> priceListItemShopRepo,
            GeneralPolicyCrmRepository generalPolicyCrmRepo,
            IShopRepository<GeneralPolicy> generalPolicyShopRepo,
            IShopRepository<Location> locationShopRepo,
            LocationCrmRepository locationCrmRepo,
            ProductCrmRepository productCrmRepo,
            IShopRepository<WebFile> webFileShopRepo,
            IShopRepository<ProductImage> productImageRepo,
            IShopRepository<Product> productRepo,
            IShopRepository<VehicleTypeProduct> vehicleTypeProductRepo,
            IShopRepository<InventoryItem> inventoryItemsRepo,
            PostCostCategoryCrmRepository postCostCategoryCrmRepo,
            IShopRepository<PostCostCategory> postCostCategoryShopRepo,
            ContactCrmRepository contactCrmRepo,
            IShopRepository<Customer> customerRepo,
            VehicleCrmRepository vehicleCrmRepo,
            IShopRepository<Vehicle> vehicleShopRepo,
            IShopRepository<OrderHistory> orderHistoryRepo,
            OrderCrmRepository orderCrmRepos,
            IShopRepository<WebPage> webPageShopRepo,
            WebPageCrmRepository webPageCrmRepo,
            IpgCrmRepository ipgCrmRepo,
            IShopRepository<Ipg> ipgShopRepo,
            IShopRepository<BankAccount> bankAccountShopRepo)
        {
            _vehicleTypeShopRepo = vehicleTypeShopRepo;
            _vehicleTypeCrmRepo = vehicleTypeCrmRepo;
            _priceListShopRepo = priceListShopRepo;
            _priceListCrmRepo = priceListCrmRepo;
            _priceListItemShopRepo = priceListItemShopRepo;
            _generalPolicyCrmRepo = generalPolicyCrmRepo;
            _generalPolicyShopRepo = generalPolicyShopRepo;
            _locationShopRepo = locationShopRepo;
            _locationCrmRepo = locationCrmRepo;
            _productCrmRepo = productCrmRepo;
            _webFileShopRepo = webFileShopRepo;
            _productImageRepo = productImageRepo;
            _productRepo = productRepo;
            _vehicleTypeProductRepo = vehicleTypeProductRepo;
            _inventoryItemsRepo = inventoryItemsRepo;
            _postCostCategoryCrmRepo = postCostCategoryCrmRepo;
            _postCostCategoryShopRepo = postCostCategoryShopRepo;
            _contactCrmRepo = contactCrmRepo;
            _customerRepo = customerRepo;
            _vehicleCrmRepo = vehicleCrmRepo;
            _vehicleShopRepo = vehicleShopRepo;
            _orderHistoryRepo = orderHistoryRepo;
            _orderCrmRepo = orderCrmRepos;
            _webPageShopRepo = webPageShopRepo;
            _webPageCrmRepo = webPageCrmRepo;
            _ipgCrmRepo = ipgCrmRepo;
            _ipgShopRepo = ipgShopRepo;
            _bankAccountShopRepo = bankAccountShopRepo;
        }

        public async Task Start()
        {
            var vehicleTypes = await this.SyncVehicleTypes();
            var priceLists = await this.SyncPriceLists();
            await this.SyncGeneralPolicy();
            await this.SyncPostCostCategories();
            await this.SyncLocations();
            var products = await this.SyncProducts();
            await this.SyncVehicleTypeProducts(products, vehicleTypes);
            await this.SyncPriceListItems(products, priceLists);
            await this.SyncInventoryItems(products);
            var contacts = await this.SyncContacts();
            await this.SyncVehicles(contacts);
            await this.SyncOrderHistory(contacts);
            await this.SyncWebPages();
            await this.SyncIpgs();
        }

        private async Task<List<CRM.Models.VehicleType>> SyncVehicleTypes()
        {
            var vehicleTypes = _vehicleTypeCrmRepo.GetActiveVehicleTypes();
            await _vehicleTypeShopRepo.AddOrUpdateRangeAsync(
                vehicleTypes.Select(vehicleType =>
                    new VehicleType
                    {
                        Id = vehicleType.Id,
                        Name = vehicleType.Name
                    }));
            await _vehicleTypeShopRepo.DeleteObsoleteRecords(vehicleTypes.Select(v => v.Id.Value).ToList());
            return vehicleTypes;
        }

        private async Task<List<CRM.Models.PriceList.PriceList>> SyncPriceLists()
        {
            var priceLists = _priceListCrmRepo.GetActivePriceLists();
            await _priceListShopRepo.AddOrUpdateRangeAsync(priceLists.Select(priceList =>
                new PriceList
                {
                    Id = priceList.Id,
                    Name = priceList.Name
                }));
            await _priceListShopRepo.DeleteObsoleteRecords(priceLists.Select(v => v.Id.Value).ToList());
            return priceLists;
        }

        private async Task SyncGeneralPolicy()
        {
            var generalPolicy = _generalPolicyCrmRepo.GetGeneralPolicy();
            await _generalPolicyShopRepo.AddOrUpdateAsync(new GeneralPolicy
            {
                Id = generalPolicy.Id,
                MaximumNumberOfPurchases = generalPolicy.MaximumNumberOfPurchases,
                PurchaseInterval = generalPolicy.PurchaseIntervalInDays,
                NumberOfPurchaseItems = generalPolicy.NumberOfPurchaseItems,
                ShowProductsOnlyRelatedToCustomerCar = generalPolicy.ShowProductsOnlyRelatedToCustomerCar,
                ApplyMaximumNumberOfPurchases = generalPolicy.ApplyMaximumNumberOfPurchases,
                ApplyPurchaseInterval = generalPolicy.ApplyPurchaseIntervalInDays,
                ApplyNumberOfPurchaseItems = generalPolicy.ApplyNumberOfPurchaseItems,
                PriceListId = generalPolicy.PriceList?.Value?.Id,
                InvoiceExpirationMin = generalPolicy.ExpireQuoteForActionMin?.Value ?? 10,
                BasketExpirationInMin = generalPolicy.ExpireQuoteForCookieMin ?? 100
            });

            await _generalPolicyShopRepo.DeleteObsoleteRecords(new List<Guid>() { generalPolicy.Id });
        }

        private async Task SyncLocations()
        {
            var locations = _locationCrmRepo.GetLocations();

            await _locationShopRepo.AddOrUpdateRangeAsync(locations.Select(location => new Location
            {
                Id = location.Id,
                Name = location.Name,
                ParentId = location.ParentLocation?.Value?.Id,
                PostCostCategoryId = location.PostCostCategory?.Value?.Id
            }).ToList());
            await _locationShopRepo.DeleteObsoleteRecords(locations.Select(l => l.Id.Value).ToList());
        }

        private async Task SyncPostCostCategories()
        {
            var postCostCategories = _postCostCategoryCrmRepo.GetActivePostCostCategories();

            await _postCostCategoryShopRepo.AddOrUpdateRangeAsync(postCostCategories.Select(p => new PostCostCategory
            {
                Id = p.Id,
                TirePostCost = p.TirePostCost?.Value?.Value
            }));
            await _postCostCategoryShopRepo.DeleteObsoleteRecords(postCostCategories.Select(p => p.Id.Value).ToList());
        }

        private async Task<List<CRM.Models.Product>> SyncProducts()
        {
            var products = _productCrmRepo.GetActiveProducts();

            var productImageIds = products
                .Select(p => p.FirstImage.Value?.Id)
                .Where(id => id != null && id != Guid.Empty)
                .ToList();
            var productImages = _productCrmRepo.GetProductImages(productImageIds);

            await _webFileShopRepo.AddOrUpdateRangeAsync(productImages.Select(a => new WebFile
            {
                Id = a.WebFile.Id,
                PartialUrl = a.PartialUrl,
                Data = a.Data,
                MimeType = a.MimeType
            }));
            await _webFileShopRepo.DeleteObsoleteRecords(productImages.Select(a => a.WebFile.Id).ToList());

            await _productRepo.AddOrUpdateRangeAsync(products.Select(product => new Product
            {
                Id = product.Id,
                Name = product.Name
            }).ToList());
            await _productRepo.DeleteObsoleteRecords(products.Select(v => v.Id.Value).ToList());

            await _productImageRepo.AddOrUpdateRangeAsync(productImages
                .Select(a => new ProductImage
                {
                    Id = a.Id,
                    ProductId = a.Product.Id,
                    ImageUrl = a.PartialUrl != null ? $"/webfile/{a.PartialUrl}" : "/not-found.png"
                }).ToList());
            await _productImageRepo.DeleteObsoleteRecords(productImages.Select(i => i.Id).ToList());

            return products;
        }


        private async Task SyncVehicleTypeProducts(List<CRM.Models.Product> products,
            List<CRM.Models.VehicleType> vehicleTypes)
        {
            var vehicleTypeProducts = _productCrmRepo.GetActiveVehicleTypeProducts();
            await _vehicleTypeProductRepo.AddOrUpdateRangeAsync(vehicleTypeProducts
                .Where(vp =>
                    vehicleTypes.Any(a => a.Id == vp.VehicleType.Value.Id) &&
                    products.Any(a => a.Id == vp.Product.Value.Id))
                .Select(v => new VehicleTypeProduct
                {
                    Id = v.Id,
                    ProductId = v.Product.Value.Id,
                    VehicleTypeId = v.VehicleType.Value.Id,
                    ProductType = v.Type.Value.Value,
                }).ToList());
            await _vehicleTypeProductRepo.DeleteObsoleteRecords(vehicleTypeProducts.Select(v => v.Id.Value).ToList());
        }

        private async Task SyncPriceListItems(List<CRM.Models.Product> products,
            List<CRM.Models.PriceList.PriceList> priceLists)
        {
            var priceListItems = _priceListCrmRepo.GetPriceListItems();
            await _priceListItemShopRepo.AddOrUpdateRangeAsync(priceListItems
                .Where(pli =>
                    priceLists.Any(a => a.Id == pli.PriceListId.Value.Id) &&
                    products.Any(a => a.Id == pli.ProductId.Value.Id))
                .Select(pi => new PriceListItem
                {
                    Id = pi.Id,
                    PriceListId = pi.PriceListId?.Value?.Id,
                    ProductId = pi.ProductId?.Value?.Id,
                    Amount = Convert.ToInt64(pi.Amount?.Value?.Value)
                }).ToList());
            await _priceListItemShopRepo.DeleteObsoleteRecords(priceListItems.Select(p => p.Id.Value).ToList());
        }

        private async Task SyncInventoryItems(List<CRM.Models.Product> products)
        {
            var inventoryItems = _productCrmRepo.GetActiveInventoryItems();
            await _inventoryItemsRepo
                .AddOrUpdateRangeAsync(inventoryItems
                    .Where(i => products.Any(a => a.Id == i.Product.Id))
                    .Select(a => new InventoryItem
                    {
                        Id = a.Id,
                        ProductId = a.Product?.Id,
                        Warehouse = a.Warehouse,
                        InventoryForSale = a.InventoryForSale,
                        ReservedInventory = a.ReservedInventory
                    }).ToList());
            await _inventoryItemsRepo.DeleteObsoleteRecords(inventoryItems.Select(p => p.Id).ToList());
        }

        private async Task<List<Contact>> SyncContacts()
        {
            var contacts = _contactCrmRepo.GetActiveContacts();

            await _customerRepo.AddOrUpdateRangeAsync(contacts.Select(c => new Customer
            {
                Id = c.Id,
                ProvinceId = c.Province?.Value?.Id,
                FirstName = c.FirstName,
                LastName = c.LastName,
                NationalId = c.NationalId,
                PostalCode = c.PostalCode,
                PostalAddress = c.PostalAddress,
                MobilePhone = c.MobilePhone,
                IsApprovedForPurchase = c.IsApprovedForPurchase
            }).ToList());

            await _customerRepo.DeleteObsoleteRecords(contacts.Select(c => c.Id.Value).ToList());
            return contacts;
        }


        private async Task SyncVehicles(List<Contact> contacts)
        {
            var vehicles = _vehicleCrmRepo.GetActiveVehicles();
            vehicles = vehicles.Where(v => contacts.Any(c => c.Id == v.Customer?.Value?.Id)).ToList();

            await _vehicleShopRepo.AddOrUpdateRangeAsync(vehicles.Select(v => new Vehicle
            {
                Id = v.Id,
                VehicleTypeId = v.VehicleType?.Value?.Id,
                CustomerId = v.Customer?.Value?.Id,
                RegistrationPlateNumberLeft = v.VehicleRegistrationNumberLeft,
                RegistrationPlateNumberMiddle = v.VehicleRegistrationNumberMiddle,
                RegistrationPlateNumberRight = v.VehicleRegistrationNumberRight,
                RegistrationPlateCharacter = v.VehicleRegistrationCharacter?.Value?.Value
            }).ToList());
            await _vehicleShopRepo.DeleteObsoleteRecords(vehicles.Select(c => c.Id.Value).ToList());
        }


        private async Task SyncOrderHistory(List<Contact> contacts)
        {
            var orders = _orderCrmRepo.GetAllOrders();
            orders = orders.Where(v => contacts.Any(c => c.Id == v.Customer?.Value?.Id)).ToList();

            await _orderHistoryRepo.AddOrUpdateRangeAsync(orders.Select(o => new OrderHistory
            {
                Id = Guid.NewGuid(),
                OrderId = o.Id,
                CustomerId = o.Customer.Value.Id,
                TotalQuantity = o.TotalQuantity,
                RegistrationDate = o.RegistrationDate
            }).ToList());
            await _orderHistoryRepo.DeleteObsoleteRecords();
        }


        private async Task SyncWebPages()
        {
            var saleInformationContent = _webPageCrmRepo.GetSaleInformationContent();
            if (saleInformationContent != null)
            {
                await _webPageShopRepo.AddOrUpdateAsync(new WebPage
                {
                    Id = saleInformationContent.Id,
                    Key = saleInformationContent.PartialUrl,
                    Data = saleInformationContent.CopyHtml ?? ""
                });
            }
        }


        private async Task SyncIpgs()
        {
            var ipgs = _ipgCrmRepo.GetIpgs();

            var bankAccounts = _ipgCrmRepo.GetBankAccounts();

            var ipgsToUpdate = ipgs.Select(item => new Ipg
            {
                Id = item.IpgId,
                ReturnUrl = item.ReturnUrl,
                Name = item.Name,
                Password = item.Password,
                PostBankAccountId = item.PostBankAccount?.Id,
                AcceptorId = item.AcceptorId,
                Bank = item.BankType,
                PassPhrase = item.PassPhrase,
                RsaKeyValue = item.RsaKeyValue,
                TerminalId = item.TerminalId,
                SequenceNumber = item.SequenceNumber,
                DisableFromHour = item.StartStopHour,
                DisableFromMinute = item.StartStopMinute,
                DisableToHour = item.FinishStopHour,
                DisableToMinute = item.FinishStopMinute

            }).ToList();
            await _ipgShopRepo.AddOrUpdateRangeAsync(ipgsToUpdate);
            await _ipgShopRepo.DeleteObsoleteRecords(ipgs.Select(c => c.IpgId).ToList());

            await _bankAccountShopRepo.AddOrUpdateRangeAsync(
                bankAccounts
                .Where(x=>ipgs.Any(i=>i.IpgId == x.Ipg?.Id))
                .Select(x => new BankAccount
            {
                Id = x.Id,
                IpgId = x.Ipg?.Id,
                Name = x.Name,
                BankName = x.BankName,
                SequenceNumber = x.SequenceNumber,
                IsPost = x.IsPost,
                Iban = x.Iban,
                ImageUrl = x.ImageUrl != null ? $"/webfile/{x.ImageUrl}" : "/bank.png"
            }).ToList());
            await _bankAccountShopRepo.DeleteObsoleteRecords(bankAccounts.Select(c => c.Id).ToList());

            await _webFileShopRepo.AddOrUpdateRangeAsync(
                bankAccounts
                .Where(x => x.Data != null && x.ImageUrl != null && x.MimeType != null)
                .Select(x => new WebFile
                {
                    Id = x.WebFile.Id,
                    PartialUrl = x.ImageUrl,
                    Data = x.Data,
                    MimeType = x.MimeType
                }).ToList());
        }
    }
}