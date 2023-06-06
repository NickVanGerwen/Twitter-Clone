using Microsoft.AspNetCore.Mvc;
using twitter_account_service.DTOs;
using twitter_account_service.Rabbitmq;

namespace twitter_account_service.Controllers
{
    [Route("api/account")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        [HttpDelete]
        public IActionResult DeleteAccount(int id)
        {
            DeletePublisher rabbitmqPublisher = new DeletePublisher();
            rabbitmqPublisher.Publish(id);
            return Ok();
        }

        [HttpPut]
        public IActionResult UpdateAccount(AccountDto account)
        {
            UpdatePublisher rabbitmqPublisher = new UpdatePublisher();
            rabbitmqPublisher.Publish(account);
            return Ok();
        }
    }
}
