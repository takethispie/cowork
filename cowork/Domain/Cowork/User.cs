namespace coworkdomain.Cowork {

    public class User {

        public User() { }


        public User(long id, string firstName, string lastName, bool isAStudent, UserType type) {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            IsAStudent = isAStudent;
            Type = type;
        }


        public long Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool IsAStudent { get; set; }
        public string Token { get; set; }
        public UserType Type { get; set; }

    }

}