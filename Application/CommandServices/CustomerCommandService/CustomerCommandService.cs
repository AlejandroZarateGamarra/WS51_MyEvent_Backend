using System.Data;
using AutoMapper;
using Domain.Publishing.Models.Commands.CustomerCommands;
using Domain.Publishing.Models.Entities;
using Domain.Publishing.Repositories;
using Domain.Publishing.Services;

namespace Application.CommandServices.CustomerCommandService;

public class CustomerCommandService:ICustomerCommandService
{
    private readonly ICustomerRepository _customerRepository;
    private readonly IMapper _mapper;
    private readonly IEncryptService _encryptService;
    private readonly ITokenService _tokenService;
    
    public CustomerCommandService(ICustomerRepository customerRepository, IMapper mapper, IEncryptService encryptService, ITokenService tokenService)
    {
        _customerRepository = customerRepository;
        _mapper = mapper;
        _encryptService = encryptService;
        _tokenService = tokenService;
    }
    
    public async Task<int> Handle(CreateCustomerCommand command)
    {
        var existingCustomers = await _customerRepository.GetAllAsync();

        var customer = _mapper.Map<CreateCustomerCommand, Customer>(command);
        return await _customerRepository.SaveAsync(customer);
    }
    
    public async Task<bool> Handle(UpdateCustomerCommand command)
    {
        var existingCustomer = await _customerRepository.GetByIdAsync(command.Id);

        var customer = _mapper.Map<UpdateCustomerCommand, Customer>(command);
        return await _customerRepository.Update(customer, command.Id);
    }
    
    public async Task<bool> Handle(UpdateCustomerPasswordCommand command)
    {
        var existingCustomer = await _customerRepository.GetByIdAsync(command.Id);

        var customer = _mapper.Map<UpdateCustomerPasswordCommand, Customer>(command);
        return await _customerRepository.UpdatePassword(customer, command.Id);
    }

    public async Task<String> Handle(SignInCommand command)
    {
        if (command == null)
            throw new ArgumentNullException(nameof(command));

        if (command.Correo == null || command.Contraseña == null)
            throw new ArgumentNullException("Correo y/o contraseña no pueden ser nulos");

        var existingCustomer = await _customerRepository.GetByEmail(command.Correo);

        if(!_encryptService.Verify(command.Contraseña, existingCustomer.ContraseñaHashed))
            throw new Exception("Usuario y/o contraseña incorrecta");

        var user = new Customer(){Id = existingCustomer.Id, Correo = existingCustomer.Correo};
        
        var token = _tokenService.GenerateToken(user);
        if (token == null)
            throw new Exception("No se pudo generar el token");

        return token;
    }

    public async Task<Customer> Handle(SignUpCommand command)
    {
        try
        {
            var existingCustomer = await _customerRepository.GetByEmail(command.Correo);
            if (existingCustomer != null) throw new DuplicateNameException("Usuario ya existe");

            var customer = new Customer()
            {
                Usuario = command.Usuario,
                Correo = command.Correo,
                ContraseñaHashed = _encryptService.Encrypt(command.Contraseña),
                ImagenUsuario = command.ImagenUsuario ?? "default.jpg",
                IsArtisan = command.IsArtisan,
                Role = command.Role ?? "User"
            };
            return await _customerRepository.Register(customer);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Excepción capturada en Handle: {ex.Message}");
            Console.WriteLine($"Pila de llamadas: {ex.StackTrace}");

            throw;
        }
    }
}

