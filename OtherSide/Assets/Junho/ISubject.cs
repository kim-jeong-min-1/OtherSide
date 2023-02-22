    public interface ISubject
    {
        // ������ ���
        void ResisterObserver(IObserver observer);
        // ������ ����
        void RemoveObserver(IObserver observer);
        //�������鿡�� ���� ����
        void NotifyObservers();
    }

    public interface IObserver
    {
        void PlayObj();
    }
