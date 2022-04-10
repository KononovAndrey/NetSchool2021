namespace DSRNetSchool.UserAccount;

using AutoMapper;
using DSRNetSchool.Common.Exceptions;
using DSRNetSchool.Common.Validator;
using DSRNetSchool.Db.Entities;
using DSRNetSchool.RabbitMQService;
using Microsoft.AspNetCore.Identity;

public class UserAccountService : IUserAccountService
{
    private readonly IMapper mapper;
    private readonly UserManager<User> userManager;
    private readonly IRabbitMqTask rabbitMqTask;
    private readonly IModelValidator<RegisterUserAccountModel> registerUserAccountModelValidator;

    public UserAccountService(IMapper mapper, UserManager<User> userManager, IRabbitMqTask rabbitMqTask, IModelValidator<RegisterUserAccountModel> registerUserAccountModelValidator)
    {
        this.mapper = mapper;
        this.userManager = userManager;
        this.rabbitMqTask = rabbitMqTask;
        this.registerUserAccountModelValidator = registerUserAccountModelValidator;
    }

    public async Task<UserAccountModel> Create(RegisterUserAccountModel model)
    {
        registerUserAccountModelValidator.Check(model);

        // Find user by email
        var user = await userManager.FindByEmailAsync(model.Email);
        if (user != null)
            throw new ProcessException($"User account with email {model.Email} already exist.");

        // Create user account
        user = new User()
        {
            Status = UserStatus.Active,
            FullName = model.Name,
            UserName = model.Email,  // Ёто логин. ћы будем его приравнивать к email, хот€ это и не об€зательно
            Email = model.Email,
            EmailConfirmed = true, // “ак как это учебный проект, то сразу считаем, что почта подтверждена. ¬ реальном проекте, скорее всего, надо будет ее подтвердить через ссылку в письме
            PhoneNumber = null,
            PhoneNumberConfirmed = false
            // ... “акже здесь есть еще интересные свойства. ѕосмотрите в документации.
        };

        var result = await userManager.CreateAsync(user, model.Password);
        if (!result.Succeeded)
            throw new ProcessException($"Creating user account is wrong. {String.Join(", ", result.Errors.Select(s => s.Description))}");


        // Send email to user
        // !!! ќбратите внимание, что мы не отправл€ем письмо, а создаем задание на его отправку. ƒальше уже все сделаетс€ само другими сервисами.
        await rabbitMqTask.SendEmail(new EmailModel() { 
            Email = model.Email,
            Subject = "DSRNetSchool",
            Message = "Your account was registered successful"
        }); 


        // Returning the created user
        return mapper.Map<UserAccountModel>(user);
    }
}
