namespace cowork.domain {

    public class User {

        public User() { }

        public User(string firstName, string lastName, bool isAStudent, UserType type) {
            FirstName = firstName;
            LastName = lastName;
            IsAStudent = isAStudent;
            Type = type;
        }


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
        public UserType Type { get; set; }

    }

}