namespace cowork.usecases {

    public interface IUseCase<out T> {

        T Execute();

    }

}