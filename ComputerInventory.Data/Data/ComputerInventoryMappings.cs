using AutoMapper;
using ComputerInventory.Core.DTOs;
using ComputerInventory.Core.Entities;

namespace ComputerInventory.Data.Data
{
    public class ComputerInventoryMappings : Profile
    {
        public ComputerInventoryMappings()
        {
            CreateMap<Inventory, InventoryDTO>().ReverseMap();

            CreateMap<PagedList<Inventory>, List<InventoryDTO>>();

            CreateMap<InventoryDTO, UserDTO>()
                .ForMember(dto => dto.ComputerLeasingTimeEnd, opt => opt.MapFrom(src => src.StartDateForCheckingInventory.AddMonths(3)));
            CreateMap<Inventory, InventoryUpdateDTO>().ReverseMap();
            CreateMap<Inventory, InventoryCreateDTO>();
            CreateMap<InventoryCreateDTO, Inventory>();

            CreateMap<User, UserDTO>();
            CreateMap<User, UserUpdateDTO>().ReverseMap();

            // För Create
            CreateMap<UserCreateDTO, User>()
                .ForMember(dest => dest.ComputerName,
                  opt => opt.MapFrom(src => GenerateComputerName(src.Position, src.Name)));

            // För Update
            CreateMap<UserUpdateDTO, User>()
                .ForMember(dest => dest.ComputerName,
                          opt => opt.MapFrom(src => GenerateComputerName(src.Position, src.Name)));
            CreateMap<User, UserGetDTO>();
        }

        

        private static string GenerateComputerName(string position, string name)
        {
            string prefix = position switch
            {
                "Developer" => "DEV",
                "Admin" => "ADMIN",
                "Manager" => "MGR",
                "Office" => "OFF",
                _ => "WORKSTATION"
            };

            var nameParts = name.Split(' ');
            string firstName = nameParts.Length > 0 && nameParts[0].Length >= 3
                ? nameParts[0].Substring(0, 3)
                : "USR";
            string lastName = nameParts.Length > 1 && nameParts[1].Length >= 3
                ? nameParts[1].Substring(0, 3)
                : "USR";

            return $"{prefix}-{firstName}{lastName}";
        }
    }
}