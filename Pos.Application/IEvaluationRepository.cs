using System.Collections.Generic;

// repository port for evaluations
public interface IEvaluationRepository
{
    void Add(Evaluation evaluation);
    List<Evaluation> GetAll();
    List<Evaluation> GetByStudentId(int studentId);

    void Update(Evaluation evaluation);
    void Remove(int id);
}
