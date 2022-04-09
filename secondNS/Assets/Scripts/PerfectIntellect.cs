using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class PerfectIntellect
{
    public float Energy = 2;
    public Genom genom;
    #region Private variebles
    readonly private List<Neuron> neurons = new List<Neuron>();
    readonly private List<Gen> gens = new List<Gen>();
    readonly private List<Neuron> inputneurons = new List<Neuron>();
    readonly private List<Neuron> outputneurons = new List<Neuron>();
    readonly private List<Neuron> calculatequeue = new List<Neuron>();
    #endregion
    #region Constants
    // Gen borders
    const float LeftGenBorder = -1;
    const float RightGenBorder = 1;
    // Bias borders
    const float LeftBiasBorder = -0.05f;
    const float RightBiasBorder = 0.05f;
    #endregion
    #region Constructors
    public PerfectIntellect(int NeuronsCount, int InputNeurons, int OutputNeurons, int GensCount)
    {
        for (int i = 0; i < NeuronsCount; i++) // Создать все нейроны
        {
            neurons.Add(new Neuron(Random.Range(LeftBiasBorder, RightBiasBorder)));
        }
        int FreeGens = GensCount - (InputNeurons + OutputNeurons); // Сколько рандомных генов
        for (int i = 0; i < GensCount; i++) // Создать все гены
        {
            gens.Add(new Gen(Random.Range(LeftGenBorder, RightGenBorder)));
        }
        for (int i = 0; i < InputNeurons; i++) // Привязать гены (по количеству входных нейронов) к входным нейронам
        {
            inputneurons.Add(neurons[i]);
            gens[i].ElementaryNeuron = neurons[i];
            gens[i].FinitieNeuron = neurons[Random.Range(InputNeurons, NeuronsCount - 1)];
        }
        for (int i = InputNeurons + FreeGens; i < NeuronsCount; i++) // Привязать гены (по количеству выходных нейронов) к выходным нейронам
        {
            outputneurons.Add(neurons[i]);
            gens[i].ElementaryNeuron = neurons[Random.Range(0, InputNeurons + FreeGens - 1)];
            gens[i].FinitieNeuron = neurons[i];
        }
        for (int i = InputNeurons; i < InputNeurons + FreeGens; i++) // Привязать рандомные гены
        {
            do
            {
                gens[i].ElementaryNeuron = neurons[Random.Range(0, NeuronsCount - 1)];
                gens[i].FinitieNeuron = neurons[Random.Range(InputNeurons, NeuronsCount - 1)];
            } while (!IsBuildsWithHimself(gens[i]));
        }
        FillCalculateQueue();
        genom = new Genom();
    }
    public PerfectIntellect(PerfectIntellect parentintellect)
    {
        neurons = parentintellect.neurons.GetRange(0, parentintellect.neurons.Count);
        gens = parentintellect.gens.GetRange(0, parentintellect.gens.Count);
        inputneurons = parentintellect.inputneurons.GetRange(0, parentintellect.inputneurons.Count);
        outputneurons = parentintellect.outputneurons.GetRange(0, parentintellect.outputneurons.Count);
        genom = new Genom(parentintellect.genom);
    }
    #endregion
    #region Public metods
    public List<float> Think(List<float> information)
    {
        for (int i = 0; i < inputneurons.Count; i++)
        {
            inputneurons[i].Container = information[i];
        }
        foreach (Neuron neuron in calculatequeue)
        {
            CalculateNeuron(neuron);
        }
        List<float> OutList = new List<float>();
        foreach (Neuron neuron in outputneurons)
        {
            OutList.Add(neuron.Container);
        }
        return OutList;
    }
    public void Mutate()
    {
        foreach (Neuron neuron in neurons)
        {
            if (Random.Range(0f, 1f) <= genom.MutateChance)
                neuron.Bias = Random.Range(LeftBiasBorder, RightBiasBorder);
        }
        foreach (Gen gen in gens)
        {
            if (Random.Range(0f, 1f) <= genom.MutateChance)
                gen.Weight = Random.Range(LeftGenBorder, RightGenBorder);
        }
        foreach (Gen gen in gens)
        {
            if (Random.Range(0f, 1f) <= genom.MutateChance / 2)
            {
                do
                {
                    gen.ElementaryNeuron = neurons[Random.Range(0, neurons.Count)];
                    gen.FinitieNeuron = neurons.Where(x => !inputneurons.Contains(x)).ToList()[Random.Range(0, neurons.Where(x => !inputneurons.Contains(x)).ToList().Count)];
                } while (!IsBuildsWithHimself(gen));
            }
        }
        genom.Mutate();
    }
    #endregion
    #region Private methods
    private bool IsBuildsWithHimself(Gen gen)
    {
        List<Neuron> IWasHere = new List<Neuron>();
        List<Neuron> Stack = new List<Neuron> { gen.FinitieNeuron };
        while (Stack.Count != 0)
        {
            Neuron Buff = Stack.Last();
            Stack.Remove(Stack.Last());
            foreach (Gen walkergen in gens.Where(x => x.FinitieNeuron == Buff))
            {
                if (walkergen.ElementaryNeuron == gen.FinitieNeuron) return true;
                if (!IWasHere.Contains(walkergen.ElementaryNeuron))
                {
                    IWasHere.Add(walkergen.ElementaryNeuron);
                    Stack.Add(walkergen.ElementaryNeuron);
                }
            }
        }
        return false;
    }
    private void CalculateNeuron(Neuron neuron) // Посчитать данный нейрон
    {
        foreach (Gen gen in gens.Where(x => x.FinitieNeuron == neuron))
        {
            neuron.Container += gen.ElementaryNeuron.Container * gen.Weight;
        }
        neuron.AddBias();
        ActivateNeuron(neuron);
    }
    private void FillCalculateQueue() // Создать очередь для подсчета нейронной сети
    {
        List<Neuron> Calculated = inputneurons.GetRange(0, inputneurons.Count);
        for (int i = 0; i < inputneurons.Count; i++)
        {
            CalculateUP(inputneurons[i], Calculated);
        }
    }
    private void CalculateUP(Neuron neuron, List<Neuron> Calculated)
    {
        foreach (Gen gen in gens.Where(x => x.ElementaryNeuron == neuron))
        {
            CalculateDown(gen.FinitieNeuron, Calculated);
            CalculateUP(gen.FinitieNeuron, Calculated);
        }
    }
    private void CalculateDown(Neuron neuron, List<Neuron> Calculated)
    {
        if (!Calculated.Contains(neuron))
        {
            foreach (Gen gen in gens.Where(x => x.FinitieNeuron == neuron))
            {
                CalculateDown(gen.ElementaryNeuron, Calculated);
            }
            calculatequeue.Add(neuron);
            Calculated.Add(neuron);
        }
    }
    private void ActivateNeuron(Neuron neuron) // Функция активации
    {
        neuron.Container = 2 / (1 + Mathf.Exp(-2 * neuron.Container)) - 1;
    }
    #endregion
}
public class Genom
{
    // Parameters
    public float AbsorbSkill;
    public float AttackSkill;
    public float DefenceSkill;

    // Скорость передвижения
    public float Speed = 0.04f;
    // Требуемое количество енергии для размножения
    public float EnergyForDublicate = 6;
    // Ежесекундное потребление енергии
    public float EatFoodPerSecond = 0.07f;
    // Радиус обнаружения
    public float DetectionRadius = 7f;
    // Скорость поворота
    public float RotateSpeed = 8f;
    // Максмум статов от абсолютного максимума
    public float MaxAttibutes = 3 / 5;
    #region Constants
    public float MutateChance = 0.005f;
    // Absorb borders
    const float LeftAbsorbBorder = 0.2f;
    const float RightAbsorbBorder = 1f;
    // Attack borders
    const float LeftAttackBorder = 0f;
    const float RightAttackBorder = 1f;
    // Defence borders
    const float LeftDefenceBorder = 0;
    const float RightDefenceBorder = 1;
    #endregion
    public Genom()
    {
        do
        {
            AbsorbSkill = Random.Range(LeftAbsorbBorder, RightAbsorbBorder);
            AttackSkill = Random.Range(LeftAttackBorder, RightAttackBorder);
            DefenceSkill = Random.Range(LeftDefenceBorder, RightDefenceBorder);
        } while (AbsorbSkill + AttackSkill + DefenceSkill > (RightAbsorbBorder + RightAttackBorder + RightDefenceBorder) * MaxAttibutes);
    }
    public Genom(Genom parent)
    {
        AbsorbSkill = parent.AbsorbSkill;
        AttackSkill = parent.AttackSkill;
        DefenceSkill = parent.DefenceSkill;
    }
    public void Mutate()
    {
        do
        {
            if (Random.Range(0f, 1f) <= MutateChance)
                AbsorbSkill = Random.Range(LeftAbsorbBorder, RightAbsorbBorder);
            if (Random.Range(0f, 1f) <= MutateChance)
                AttackSkill = Random.Range(LeftAttackBorder, RightAttackBorder);
            if (Random.Range(0f, 1f) <= MutateChance)
                DefenceSkill = Random.Range(LeftDefenceBorder, RightDefenceBorder);
        } while (AbsorbSkill + AttackSkill + DefenceSkill > (RightAbsorbBorder + RightAttackBorder + RightDefenceBorder) * MaxAttibutes);
    }
}
public class Neuron
{
    public float Container = 0;
    public float Bias = 0;
    public Neuron(float Bias)
    {
        this.Bias = Bias;
    }
    public void AddBias()
    {
        Container += Bias;
    }
}
public class Gen
{
    public Neuron ElementaryNeuron;
    public float Weight;
    public Neuron FinitieNeuron;
    public Gen(float Weight)
    {
        this.Weight = Weight;
    }
}