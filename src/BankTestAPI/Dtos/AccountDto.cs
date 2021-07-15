namespace BankTestAPI.Dtos
{
    public class AccountDto
    {
        public int Id { get; set; }

        public decimal Balance { get; private set; }

        public int OwnerId { get; set; }

        public bool IsEmpty()
        {
            return Id == 0 && Balance == 0 && OwnerId == 0;
        }
    }
}