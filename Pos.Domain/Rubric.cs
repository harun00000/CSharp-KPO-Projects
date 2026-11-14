using System;
using System.Collections.Generic;

// domain model
// Ubiquitous Language: Rubric - рубрикатор критериев
public class Rubric
{
    private readonly List<Criterion> criteria;

    public Rubric()
    {
        criteria = new List<Criterion>();
    }

    public bool Contains(Criterion criterion)
    {
        foreach (Criterion item in criteria)
        {
            if (item.Id == criterion.Id)
            {
                return true;
            }
        }

        return false;
    }
    public void Add(Criterion criterion)
    {
        if (Contains(criterion))
        {
            // invariant: rubric can't contain criteria with the same id
            throw new ArgumentException("Такой критерий уже есть:(");
        }

        criteria.Add(criterion);
    }

    public List<Criterion> GetAll()
    {
        // return copy (because we can't change them)
        return new List<Criterion>(criteria);
    }

    public Criterion? GetById(int id)
    {
        foreach (Criterion criterion in criteria)
        {
            if (criterion.Id == id)
            {
                return criterion;
            }
        }

        return null;
    }
}