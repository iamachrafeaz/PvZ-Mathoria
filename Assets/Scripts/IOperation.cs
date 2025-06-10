public interface IOperation
{
    void GenerateOperation();
    bool GetIsAnswered();
    void SetIsAnswered(bool IsAnswered);
    string GetQuestion();
}
