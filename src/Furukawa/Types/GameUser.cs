﻿using Bunkum.HttpServer.RateLimit;
using Realms;

namespace Furukawa.Types
{
    public class GameUser : RealmObject, IRateLimitUser
    {
        [PrimaryKey] [Required] public string Id { get; init; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string PasswordBcrypt { get; set; }
        public DateTimeOffset CreationDate { get; init; }
        public int SelectedSkin { get; set; }
        public UserStatistics Statistics { get; set; }
        [Backlink(nameof(GameSession.User))] public IQueryable<GameSession> Sessions { get; }

        // Defined in authentication provider. Avoids Realm threading nonsense.
        public bool RateLimitUserIdIsEqual(object obj)
        {
            if (obj is not string id) return false;
            return Id == id;
        }

        [Ignored] public object RateLimitUserId { get; internal set; } = null!;
    }
}