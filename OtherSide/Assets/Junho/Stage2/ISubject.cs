    public interface ISubject
    {
        // 옵저버 등록
        void ResisterObserver(IObserver observer);
        // 옵저버 해지
        void RemoveObserver(IObserver observer);
        //옵저버들에게 내용 전달
        void NotifyObservers();
    }

    public interface IObserver
    {
        void PlayObj();
    }
