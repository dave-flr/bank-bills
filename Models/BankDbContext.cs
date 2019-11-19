using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace bank_bills.Models
{
    public class BankDbContext : IdentityDbContext
    {
        public BankDbContext(DbContextOptions<BankDbContext> options) : base(options)
        {
        }

        public DbSet<CheckingAccount> CheckingAccounts { get; set; }
        public DbSet<SavingAccount> SavingAccounts { get; set; }
        public DbSet<JuridicPerson> JuridicPersons { get; set; }
        public DbSet<NaturalPerson> NaturalPersons { get; set; }
        public DbSet<WithdrawalCertificate> WithdrawalCertificates { get; set; }
        public DbSet<DepositCertificate> DepositCertificates { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity("bank_bills.Models.CheckingAccount", b =>
            {

            });
            modelBuilder.Entity("bank_bills.Models.SavingAccount", b =>
            {

            });
            modelBuilder.Entity("bank_bills.Models.JuridicPerson", b =>
            {
                // b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                //     .WithOne()
                //     .HasForeignKey("UserId")
                //     .OnDelete(DeleteBehavior.Cascade)
                //     .IsRequired();
            });
            modelBuilder.Entity("bank_bills.Models.NaturalPerson", b =>
            {
                // b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", "User")
                //     .WithOne(np => np.)
                //     .HasForeignKey("UserId")
                //     .OnDelete(DeleteBehavior.Cascade)
                //     .IsRequired();
            });
            modelBuilder.Entity("bank_bills.Models.WithdrawalCertificate", b =>
            {

            });
            modelBuilder.Entity("bank_bills.Models.DepositCertificate", b =>
            {

            });

            // modelBuilder.Entity<JuridicPerson>()
            //     .HasOne<IdentityUser>(u => u.User)
            //     .WithOne(jp => jp.)
            //     .HasForeignKey<IdentityUser>(iu => iu.Id);

            modelBuilder.Entity<IdentityUser>()
                .HasOne<NaturalPerson>()
                .WithOne(iu => iu.User)
                .HasForeignKey<NaturalPerson>(np => np.UserId)
                .IsRequired();

            modelBuilder.Entity<IdentityUser>()
                .HasOne<JuridicPerson>()
                .WithOne(iu => iu.User)
                .HasForeignKey<JuridicPerson>(np => np.UserId)
                .IsRequired();
        }
    }
}