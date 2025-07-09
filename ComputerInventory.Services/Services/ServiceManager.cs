using AutoMapper;
using ComputerInventory.Core.Repositories;
using Service.Contracts.Interfaces;

namespace ComputerInventory.Services.Services;

public class ServiceManager : IServiceManager
{
    private readonly Lazy<IInventoryService> _inventoryService;
    private readonly Lazy<IUserService> _userService;

    public ServiceManager(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _inventoryService = new Lazy<IInventoryService>(() => new InventoryService(unitOfWork, mapper));
        _userService = new Lazy<IUserService>(() => new UserService(unitOfWork, mapper));
    }

    public IInventoryService InventoryService => _inventoryService.Value;
    public IUserService UserService => _userService.Value;
}