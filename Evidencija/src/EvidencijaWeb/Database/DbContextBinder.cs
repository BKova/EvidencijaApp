///Created by: Bartul Kovačić
///Github: https:github.com/BKova
using System;
using System.Collections.Generic;
using System.Linq;

namespace Evidencija.Database.Models
{
    public interface IDbContextBinder 
    {
        User CreateUser(User User);

        User ModifyUser(User User);

        bool DeleteUser(int UserId);

        ICollection<User> GetAllUsers();

        User GetUser(string UserName, int KeyCode);

        User GetUser(int userId);

        ICollection<TimeStamp> UserTimeStamps(User User);

        ICollection<TimeStamp> UserTimeStamps(ICollection<User> User);

        TimeStamp CreateTimeStamp(TimeStamp TimeStamp);

        TimeStamp ModifyTimeStamp(TimeStamp TimeStamp);

        TimeStamp GetTimeStamp(int TimeStampId);

        bool DeleteTimeStamp(int TimeStampId);
    }

    public class DbContextBinder : IDbContextBinder
    {
        private EvidencijaDbContext _evidencijaDbContext;

        public DbContextBinder(EvidencijaDbContext EvidencijaDbContext)
        {
            _evidencijaDbContext = EvidencijaDbContext;
        }

        public TimeStamp CreateTimeStamp(TimeStamp TimeStamp)
        {
            try
            {
                _evidencijaDbContext.Stamps.Add(TimeStamp);
                _evidencijaDbContext.SaveChanges();
            }
            catch(Exception ex)
            {
                return new TimeStamp();
            }
            return TimeStamp;
        }

        public User CreateUser(User User)
        {
            if (_evidencijaDbContext.Users.Any(u => u.UserName == User.UserName)) return new User();

            try
            {
                _evidencijaDbContext.Users.Add(User);
                _evidencijaDbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                return new User();
            }
            return User;
        }

        public bool DeleteTimeStamp(int TimeStampId)
        {
            try
            {
                _evidencijaDbContext.Stamps.Remove(_evidencijaDbContext.Stamps.Where(s => s.Id == TimeStampId).SingleOrDefault());
                _evidencijaDbContext.SaveChanges();
            }
            catch(Exception ex)
            {
                return false;
            }
            return true;
        }

        public bool DeleteUser(int UserId)
        {
            try
            {
                _evidencijaDbContext.Users.Remove(_evidencijaDbContext.Users.Where(u => u.Id == UserId).SingleOrDefault());
                _evidencijaDbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                return false;
            }
            return true;
        }

        public ICollection<User> GetAllUsers()
        {
           return _evidencijaDbContext.Users.ToList();
        }

        public TimeStamp GetTimeStamp(int TimeStampId)
        {
            try
            {
                return _evidencijaDbContext.Stamps.Find(TimeStampId);
            }
            catch (Exception ex)
            {
            }
            return new TimeStamp();
        }

        public User GetUser(int userId)
        {
            return _evidencijaDbContext.Users.Find(userId);
        }

        public User GetUser(string UserName, int KeyCode)
        {
            try
            {
                return _evidencijaDbContext.Users.Where(x => x.UserName == UserName && x.LoginKey == KeyCode).SingleOrDefault();
            }
            catch (Exception ex)
            {
            }
            return new User();
        }

        public TimeStamp ModifyTimeStamp(TimeStamp TimeStamp)
        {
            try
            {
                var stamp =_evidencijaDbContext.Stamps.Find(TimeStamp.Id);

                stamp.Start = TimeStamp.Start;
                stamp.End = TimeStamp.End;
                stamp.Duration = TimeStamp.Duration;
                stamp.Closed = TimeStamp.Closed;
                _evidencijaDbContext.SaveChanges();
                return stamp;
            }
            catch (Exception ex)
            {
                return new TimeStamp();
            }
        }

        public User ModifyUser(User User)
        {
            try
            {
                var user = _evidencijaDbContext.Users.Find(User);

                user.UserName = User.UserName;
                user.LoginKey = User.LoginKey;
                _evidencijaDbContext.SaveChanges();
                return user;
            }
            catch(Exception ex)
            {
                return new User();
            }
        }

        public ICollection<TimeStamp> UserTimeStamps(ICollection<User> Users)
        {
            List<TimeStamp> usersTimeStamps = new List<TimeStamp>();
            foreach (var user in Users)
                usersTimeStamps.AddRange(_evidencijaDbContext.Stamps.Where(s => s.User.Id == user.Id).ToList());
            return usersTimeStamps;

        }

        public ICollection<TimeStamp> UserTimeStamps(User User)
        {
            return _evidencijaDbContext.Stamps.Where(s => s.User.Id == User.Id).ToList();
        }
    }
}
