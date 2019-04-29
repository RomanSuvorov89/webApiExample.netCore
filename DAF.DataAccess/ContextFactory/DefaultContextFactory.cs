using System;
using System.Collections.Generic;
using System.Linq;
using DAF.DataAccess.Models;
using DAF.DataAccess.UnitOfWork;

namespace DAF.DataAccess.ContextFactory
{
    public class DefaultContextFactory : IContextFactory
    {
        private readonly DataDbContext _context;

        public DefaultContextFactory()
        {
            _context = new DataDbContext();
            InitDefaultUsers();
        }

        public IUnitOfWork GetContext()
        {
            return new UnitOfWorkEF(_context);
        }

        private void InitDefaultUsers()
        {
            var usersDb = _context.Users.ToList();

            if (!usersDb.Any())
            {
                var newUsers = new List<User>
                {
                    new User
                  {
                    FirstName = "Анастасия",
                    MiddleName = "Кадочникова",
                    LastName = "Андреевна",
                    Email = "stasha@token.ru",
                    Login = "Stasha",
                    Password = "506.Nast19ya!",
                    Id = new Guid("B5184F8B-B202-4172-88D9-6B90D899ED24"),
                    AcсessTokens = new List<UserToken>
                    {
                        new UserToken
                        {
                            Id = Guid.NewGuid(),
                            ExpiresAt = DateTime.Now.AddHours(1)
                        },
                        new UserToken
                        {
                            Id = Guid.NewGuid(),
                            ExpiresAt = DateTime.Now.AddHours(2)
                        }
                    }
                },
                    new User
                {
                    FirstName = "Наталья",
                    MiddleName = "Давыдова",
                    LastName = "Артёмовна",
                    Email = "Nataly@token.ru",
                    Login = "Nataly",
                    Password = "Nata59Perm",
                    Id = new Guid("72994B74-A029-4662-94D4-99EAD9BED2CB"),
                    AcсessTokens = new List<UserToken>
                    {
                        new UserToken
                        {
                            Id = Guid.NewGuid(),
                            ExpiresAt = DateTime.Now.AddHours(1)
                        },
                        new UserToken
                        {
                            Id = Guid.NewGuid(),
                            ExpiresAt = DateTime.Now.AddHours(2)
                        }
                    }
                },
                    new User
                {
                    FirstName = "Елена",
                    MiddleName = "Калашникова",
                    LastName = "Николаевна",
                    Email = "Lenchik@token.ru",
                    Login = "Lenchik",
                    Password = "Lena5uper!e1en",
                    Id = new Guid("52F82151-3DC7-4FF5-B591-D86556DAFA13"),
                    AcсessTokens = new List<UserToken>
                    {
                        new UserToken
                        {
                            Id = Guid.NewGuid(),
                            ExpiresAt = DateTime.Now.AddHours(1)
                        },
                        new UserToken
                        {
                            Id = Guid.NewGuid(),
                            ExpiresAt = DateTime.Now.AddHours(2)
                        }
                    }
                }
                };

                _context.Users.AddRange(newUsers);
                _context.SaveChanges();
            }
        }
    }
}