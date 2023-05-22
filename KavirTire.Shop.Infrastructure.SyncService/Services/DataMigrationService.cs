using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KavirTire.Shop.Infrastructure.SyncService.CRM.Models.Contact;
using KavirTire.Shop.Infrastructure.SyncService.CRM.Repository;
using KavirTire.Shop.Infrastructure.SyncService.Shop.Models;
using KavirTire.Shop.Infrastructure.SyncService.Shop.Persistence;
using NLog;
using NLog.Fluent;
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
        private readonly CustomerRepository _customerRepo;

        private readonly VehicleCrmRepository _vehicleCrmRepo;
        private readonly IShopRepository<Vehicle> _vehicleShopRepo;

        private readonly OrderHistoryRepository _orderHistoryRepo;
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
            CustomerRepository customerRepo,
            VehicleCrmRepository vehicleCrmRepo,
            IShopRepository<Vehicle> vehicleShopRepo,
            OrderHistoryRepository orderHistoryRepo,
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
            logger.Info("Migrating data from CRM to shop");
            var vehicleTypes = await this.SyncVehicleTypes();
            var priceLists = await this.SyncPriceLists();
            await this.SyncGeneralPolicy();
            await this.SyncPostCostCategories();
            await this.SyncLocations();
            var products = await this.SyncProducts();
            await this.SyncVehicleTypeProducts(products, vehicleTypes);
            await this.SyncPriceListItems(products, priceLists);
            await this.SyncInventoryItems(products);
             await this.SyncContacts();
            await this.SyncVehicles();
            await this.SyncOrderHistory();
            await this.SyncWebPages();
            await this.SyncIpgs();
        }

        private async Task<List<CRM.Models.VehicleType>> SyncVehicleTypes()
        {
            logger.Info("SyncVehicleTypes Started.");
            
            var vehicleTypes = _vehicleTypeCrmRepo.GetActiveVehicleTypes();
            logger.Info($"Found {vehicleTypes.Count} VehicleTypes.");
            
            logger.Info("Updating VehicleTypes.");
            await _vehicleTypeShopRepo.AddOrUpdateRangeAsync(
                vehicleTypes.Select(vehicleType =>
                    new VehicleType
                    {
                        Id = vehicleType.Id,
                        Name = vehicleType.Name
                    }));
            
            logger.Info("Deleting Obsolete VehicleTypes.");
            await _vehicleTypeShopRepo.DeleteObsoleteRecords(vehicleTypes.Select(v => v.Id.Value).ToList());
            
            logger.Info("SyncVehicleTypes Finished.");
            return vehicleTypes;
        }

        private async Task<List<CRM.Models.PriceList.PriceList>> SyncPriceLists()
        {
            logger.Info("SyncPriceLists Started.");
            
            var priceLists = _priceListCrmRepo.GetActivePriceLists();
            logger.Info($"Found {priceLists.Count} PriceLists.");
            
            logger.Info("Updating PriceLists.");
            await _priceListShopRepo.AddOrUpdateRangeAsync(priceLists.Select(priceList =>
                new PriceList
                {
                    Id = priceList.Id,
                    Name = priceList.Name
                }));
            
            logger.Info("Deleting Obsolete PriceLists.");
            await _priceListShopRepo.DeleteObsoleteRecords(priceLists.Select(v => v.Id.Value).ToList());
            
            logger.Info("SyncPriceLists Finished.");
            return priceLists;
        }

        private async Task SyncGeneralPolicy()
        {
            
            logger.Info("SyncGeneralPolicy Started.");
            
            var generalPolicy = _generalPolicyCrmRepo.GetGeneralPolicy();
            logger.Info("Updating GeneralPolicy.");

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

            logger.Info("Deleting Obsolete GeneralPolicy.");
            await _generalPolicyShopRepo.DeleteObsoleteRecords(new List<Guid>() { generalPolicy.Id });
            
            logger.Info("SyncGeneralPolicy Finished.");

        }

        private async Task SyncLocations()
        {
            
            logger.Info("SyncLocations Started.");
            
            var locations = _locationCrmRepo.GetLocations();
            logger.Info($"Found {locations.Count} Locations.");

            logger.Info("Updating Locations.");
            await _locationShopRepo.AddOrUpdateRangeAsync(locations.Select(location => new Location
            {
                Id = location.Id,
                Name = location.Name,
                ParentId = location.ParentLocation?.Value?.Id,
                PostCostCategoryId = location.PostCostCategory?.Value?.Id
            }).ToList());
            
            logger.Info("Deleting Obsolete Locations.");
            await _locationShopRepo.DeleteObsoleteRecords(locations.Select(l => l.Id.Value).ToList());
            
            logger.Info("SyncLocations Finished.");
        }

        private async Task SyncPostCostCategories()
        {
            logger.Info("SyncPostCostCategories Started.");
            
            var postCostCategories = _postCostCategoryCrmRepo.GetActivePostCostCategories();
            logger.Info($"Found {postCostCategories.Count} PostCostCategories.");

            logger.Info("Updating PostCostCategories.");
            await _postCostCategoryShopRepo.AddOrUpdateRangeAsync(postCostCategories.Select(p => new PostCostCategory
            {
                Id = p.Id,
                TirePostCost = p.TirePostCost?.Value?.Value
            }));
            
            logger.Info("Deleting Obsolete PostCostCategories.");
            await _postCostCategoryShopRepo.DeleteObsoleteRecords(postCostCategories.Select(p => p.Id.Value).ToList());
            
            logger.Info("SyncPostCostCategories Finished.");
        }

        private async Task<List<CRM.Models.Product>> SyncProducts()
        {
            
            logger.Info("SyncProducts Started.");

            var products = _productCrmRepo.GetActiveProducts();
            logger.Info($"Found {products.Count} Products.");
            
            var productImageIds = products
                .Select(p => p.FirstImage.Value?.Id)
                .Where(id => id != null && id != Guid.Empty)
                .ToList();
            var productImages = _productCrmRepo.GetProductImages(productImageIds);
            logger.Info($"Found {productImages.Count} ProductImages.");
            
            logger.Info($"Updating Shop WebFiles.");
            await _webFileShopRepo.AddOrUpdateRangeAsync(productImages.Select(a => new WebFile
            {
                Id = a.WebFile.Id,
                PartialUrl = a.PartialUrl,
                Data = a.Data,
                MimeType = a.MimeType
            }));
            
            logger.Info("Deleting Obsolete WebFiles.");
            await _webFileShopRepo.DeleteObsoleteRecords(productImages.Select(a => a.WebFile.Id).ToList());
            
            logger.Info($"Updating Products.");
            await _productRepo.AddOrUpdateRangeAsync(products.Select(product => new Product
            {
                Id = product.Id,
                Name = product.Name
            }).ToList());
            
            logger.Info("Deleting Obsolete Products.");
            await _productRepo.DeleteObsoleteRecords(products.Select(v => v.Id.Value).ToList());

            await _productImageRepo.AddOrUpdateRangeAsync(productImages
                .Select(a => new ProductImage
                {
                    Id = a.Id,
                    ProductId = a.Product.Id,
                    ImageUrl = a.PartialUrl != null ? $"/webfile/{a.PartialUrl}" : "/not-found.png"
                }).ToList());
            logger.Info("Updating Product Images.");
            
            logger.Info("Deleting Obsolete ProductImages.");
            await _productImageRepo.DeleteObsoleteRecords(productImages.Select(i => i.Id).ToList());
            
            logger.Info("SyncProducts Finished.");
            
            return products;
        }


        private async Task SyncVehicleTypeProducts(List<CRM.Models.Product> products,
            List<CRM.Models.VehicleType> vehicleTypes)
        {
            logger.Info("SyncVehicleTypeProducts Started.");
            
            var vehicleTypeProducts = _productCrmRepo.GetActiveVehicleTypeProducts();
            logger.Info($"Found {vehicleTypeProducts.Count} VehicleTypeProducts.");

            logger.Info("Updating VehicleTypeProducts.");
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
            
            logger.Info("Deleting Obsolete VehicleTypeProducts.");
            await _vehicleTypeProductRepo.DeleteObsoleteRecords(vehicleTypeProducts.Select(v => v.Id.Value).ToList());
            
            logger.Info("SyncVehicleTypeProducts Finished.");
        }

        private async Task SyncPriceListItems(List<CRM.Models.Product> products,
            List<CRM.Models.PriceList.PriceList> priceLists)
        {
            logger.Info("SyncPriceListItems Started.");
            
            var priceListItems = _priceListCrmRepo.GetPriceListItems();
            logger.Info($"Found {priceListItems.Count} PriceListItems.");

            logger.Info("Updating PriceListItems.");
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
            
            logger.Info("Deleting Obsolete PriceListItems.");
            await _priceListItemShopRepo.DeleteObsoleteRecords(priceListItems.Select(p => p.Id.Value).ToList());
            
            logger.Info("SyncPriceListItems Finished.");

        }

        private async Task SyncInventoryItems(List<CRM.Models.Product> products)
        {
            logger.Info("SyncInventoryItems Started.");
            
            var inventoryItems = _productCrmRepo.GetActiveInventoryItems();
            logger.Info($"Found {inventoryItems.Count} InventoryItems.");

            logger.Info("Updating InventoryItems.");
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
            
            logger.Info("Deleting Obsolete InventoryItems.");
            await _inventoryItemsRepo.DeleteObsoleteRecords(inventoryItems.Select(p => p.Id).ToList());
            
            logger.Info("SyncInventoryItems Finished.");
        }

        private async Task SyncContacts()
        {
            logger.Info("SyncContacts Started.");
            
            logger.Info("Finding last synced Contact/Customer.");
            var lastUpdatedCustomer = _customerRepo.GetLastUpdatedCustomer();
            
            var contacts = _contactCrmRepo.GetActiveContacts(lastUpdatedCustomer?.CrmRowVersion);
            logger.Info($"Found {contacts.Count} newly Created/Updated Contacts/Customers.");

            
            logger.Info("Updating Contacts/Customers.");
            await _customerRepo.AddOrUpdateRangeAsync(contacts.Select(c => new Customer
            {
                Id = c.Id,
                ProvinceId = c.Province?.Value?.Id,
                FirstName = c.FirstName,
                LastName = c.LastName,
                Username = c.AdxUsername,
                NationalId = c.NationalId,
                PostalCode = c.PostalCode,
                PostalAddress = c.PostalAddress,
                MobilePhone = c.MobilePhone,
                CrmRowVersion = c.VersionNumber,
                IsApprovedForPurchase = c.IsApprovedForPurchase
            }).ToList());
            
            logger.Info("SyncContacts Finished.");
        }


        private async Task SyncVehicles()
        {
            logger.Info("SyncVehicles Started.");

            
            var customers = await _customerRepo.ListAsync();
            var vehicles = _vehicleCrmRepo.GetActiveVehicles();
            vehicles = vehicles.Where(v => customers.Any(c => c.Id == v.Customer?.Value?.Id)).ToList();
            logger.Info($"Found {vehicles.Count} Vehicles.");

            logger.Info("Updating Vehicles.");
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
            
            logger.Info("Deleting Obsolete Vehicles.");
            await _vehicleShopRepo.DeleteObsoleteRecords(vehicles.Select(c => c.Id.Value).ToList());
            
            logger.Info("SyncVehicles Finished.");
        }


        private async Task SyncOrderHistory()
        {
            
            logger.Info("SyncOrderHistory Started.");
            
            logger.Info("Finding last synced OrderHistory.");
            var lastUpdatedOrderHistory = _orderHistoryRepo.GetLastUpdatedOrderHistory();
            
            var orders = _orderCrmRepo.GetAllOrders(lastUpdatedOrderHistory?.CrmRowVersion);
            logger.Info($"Found {orders.Count} newly Created/Updated Orders.");

            
            logger.Info("Updating Contacts/Customers.");
            await _orderHistoryRepo.AddOrUpdateRangeAsync(
                orders.Select(o => new OrderHistory
                {
                    Id = Guid.NewGuid(),
                    OrderId = o.Id,
                    CustomerId = o.Customer.Value.Id,
                    TotalQuantity = o.TotalQuantity,
                    RegistrationDate = o.RegistrationDate,
                    CrmRowVersion = o.VersionNumber
                }).ToList());
            
            logger.Info("SyncOrderHistory Finished.");
        }


        private async Task SyncWebPages()
        {
            logger.Info("SyncWebPages Started.");
            
            logger.Info($"Syncing SaleInformationContent Page.");
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
            
            logger.Info("SyncWebPages Finished.");
        }


        private async Task SyncIpgs()
        {
            
            logger.Info("SyncIpgs Started.");
            
         
            
            
            
            var ipgs = _ipgCrmRepo.GetIpgs();
            logger.Info($"Found {ipgs.Count} IPGs.");

            var bankAccounts = _ipgCrmRepo.GetBankAccounts();
            logger.Info($"Found {bankAccounts.Count} BankAccounts.");

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
            
            logger.Info($"Updating IPGs.");
            await _ipgShopRepo.AddOrUpdateRangeAsync(ipgsToUpdate);
            
            logger.Info($"Deleting Obsolete IPGs.");
            await _ipgShopRepo.DeleteObsoleteRecords(ipgs.Select(c => c.IpgId).ToList());

            logger.Info($"Updating BankAccounts.");
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
            
            logger.Info($"Deleting Obsolete BankAccounts.");
            await _bankAccountShopRepo.DeleteObsoleteRecords(bankAccounts.Select(c => c.Id).ToList());

            
            logger.Info($"Updating BankAccount WebFiles.");
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
            
            logger.Info("SyncIpgs Finished.");
        }
    }
}