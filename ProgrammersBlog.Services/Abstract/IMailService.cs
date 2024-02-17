using ProgrammersBlog.Entities.Dtos;
using ProgrammersBlog.Shared.Results;

namespace ProgrammersBlog.Services.Abstract
{
    public interface IMailService
    {
        IResult Send(EmailSendDto emailSendDto);
        IResult SendContactEmail(EmailSendDto emailSendDto);
    }
}
