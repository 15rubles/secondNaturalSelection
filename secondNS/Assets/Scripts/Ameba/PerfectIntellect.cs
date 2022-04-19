using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class PerfectIntellect
{
    public int AllNeuronsCount;
    public int AllGensCount;
    public int InputNeuronsCount;
    public int OutputNeuronsCount;

    public float LifeTime = 0;
    public float Energy = 2;
    public Genom genom;
    #region Private variebles
    public List<Neuron> neurons = new List<Neuron>();
    public List<Gen> gens = new List<Gen>();
    readonly public List<Neuron> inputneurons = new List<Neuron>();
    readonly public List<Neuron> outputneurons = new List<Neuron>();
    readonly public List<Neuron> calculatequeue = new List<Neuron>();
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
    public PerfectIntellect(int InputNeurons, int InnerNeurons, int OutputNeurons, int GensCount)
    {
        InputNeuronsCount = InputNeurons;
        OutputNeuronsCount = OutputNeurons;
        AllGensCount = GensCount;
        AllNeuronsCount = InputNeurons + InnerNeurons + OutputNeurons;
        for (int i = 0; i < AllNeuronsCount; i++) // Создать все нейроны
        {
            neurons.Add(new Neuron(Random.Range(LeftBiasBorder, RightBiasBorder)));
        }
        for (int i = 0; i < GensCount; i++) // Создать все гены
        {
            gens.Add(new Gen(Random.Range(LeftGenBorder, RightGenBorder)));
        }
        for (int i = 0; i < InputNeurons; i++) // Найти входные нейроны и привязять гены к ним(по кол-ву нейронов)
        {
            inputneurons.Add(neurons[i]);

            gens[i].ElementaryNeuron = neurons[i];
            gens[i].ElementaryNeuronNumberInList = i;

            int rand = Random.Range(InputNeurons, AllNeuronsCount - 1);
            gens[i].FinitieNeuron = neurons[rand];
            gens[i].FinitieNeuronNumberInList = rand;
        }
        for (int i = InputNeurons; i < InputNeurons + OutputNeurons; i++) // Привязать гены (по количеству выходных нейронов) к выходным нейронам
        {
            do
            {
                outputneurons.Add(neurons[i]);

                int rand = Random.Range(InputNeurons, AllNeuronsCount - 1);
                gens[i].ElementaryNeuron = neurons[rand];
                gens[i].ElementaryNeuronNumberInList = rand;

                gens[i].FinitieNeuron = neurons[i];
                gens[i].FinitieNeuronNumberInList = i;
            } while (IsBuildsWithHimself(gens[i]));
        }
        for (int i = InputNeurons + OutputNeurons; i < GensCount; i++) // Привязать рандомные гены
        {
            do
            {
                int rand = Random.Range(InputNeurons, AllNeuronsCount - 1);
                gens[i].ElementaryNeuron = neurons[rand];
                gens[i].ElementaryNeuronNumberInList = rand;

                rand = Random.Range(InputNeurons, AllNeuronsCount - 1);
                gens[i].FinitieNeuron = neurons[rand];
                gens[i].FinitieNeuronNumberInList = rand;
            } while (IsBuildsWithHimself(gens[i]));
        }
        FillCalculateQueue();
        genom = new Genom();
    }
    public PerfectIntellect(PerfectIntellect parentintellect)
    {
        neurons = new List<Neuron>();
        foreach (Neuron neuron in parentintellect.neurons)
        {
            neurons.Add(new Neuron(neuron));
        }
        gens = new List<Gen>();
        foreach (Gen gen in parentintellect.gens)
        {
            gens.Add(new Gen(gen));
            //gens.Last().BindToNeurons(neurons);
        }
        AllNeuronsCount = parentintellect.AllNeuronsCount;
        AllGensCount = parentintellect.AllGensCount;
        InputNeuronsCount = parentintellect.InputNeuronsCount;
        OutputNeuronsCount = parentintellect.OutputNeuronsCount;
        ReloadAfterBirth();
        genom = new Genom(parentintellect.genom);
        FillCalculateQueue();
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
    public void ReloadAfterBirth()
    {
        inputneurons.Clear();
        outputneurons.Clear();
        for (int i = 0; i < InputNeuronsCount; i++)
        {
            inputneurons.Add(neurons[i]);
        }
        for (int i = InputNeuronsCount; i < InputNeuronsCount + OutputNeuronsCount; i++)
        {
            outputneurons.Add(neurons[i]);
        }
        foreach (Gen gen in gens)
        {
            gen.ElementaryNeuron = neurons[gen.ElementaryNeuronNumberInList];
            gen.FinitieNeuron = neurons[gen.FinitieNeuronNumberInList];
        }
        FillCalculateQueue();
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
    public int Generation;
    public float SecondName;
    // Parameters
    public float AbsorbSkill;
    public float AttackSkill;
    public float DefenceSkill;

    // Attack coeficients
    public float AttackBiteCoeficient = 1.25f;
    public float AttackSuckCoeficient = 0.0375f;
    public float EatSuckCoeficient = 0.03f;

    // Скорость передвижения
    public float Speed = 0.05f;
    // Требуемое количество енергии для размножения
    public float EnergyForDublicate = 6;
    // Ежесекундное потребление енергии
    public float EatFoodPerSecond = 0.2f;
    // Радиус обнаружения
    public float DetectionRadius = 12f;
    // Скорость поворота
    public float RotateSpeed = 8f;
    // Максмум статов от абсолютного максимума
    public float MaxAttibutes = 3.5f / 5f;
    #region Constants
    public float MutateChance = 0.01f;
    // Absorb borders
    public const float LeftAbsorbBorder = 0.2f;
    public const float RightAbsorbBorder = 1f;
    // Attack borders
    public const float LeftAttackBorder = 0f;
    public const float RightAttackBorder = 1f;
    // Defence borders
    public const float LeftDefenceBorder = 0;
    public const float RightDefenceBorder = 1;
    #endregion
    public Genom()
    {
        Generation = 0;
        SecondName = Random.Range(0f, 100f);
        do
        {
            AbsorbSkill = Random.Range(LeftAbsorbBorder, RightAbsorbBorder);
            AttackSkill = Random.Range(LeftAttackBorder, RightAttackBorder);
            DefenceSkill = Random.Range(LeftDefenceBorder, RightDefenceBorder);
        } while (AbsorbSkill + AttackSkill + DefenceSkill > (RightAbsorbBorder + RightAttackBorder + RightDefenceBorder) * MaxAttibutes);
    }
    public Genom(Genom parent)
    {
        Generation = parent.Generation + 1;
        SecondName = parent.SecondName;
        AbsorbSkill = parent.AbsorbSkill;
        AttackSkill = parent.AttackSkill;
        DefenceSkill = parent.DefenceSkill;
    }
    public void Mutate()
    {
        if(Random.Range(0f, 1f) <= MutateChance * 4)
        {
            SecondName = Random.Range(0f, 100f);
        }
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
    public Neuron(Neuron neuron)
    {
        Bias = neuron.Bias;
    }
    public void AddBias()
    {
        Container += Bias;
    }
}
public class Gen
{
    public int ElementaryNeuronNumberInList;
    public int FinitieNeuronNumberInList;
    public Neuron ElementaryNeuron;
    public float Weight;
    public Neuron FinitieNeuron;
    public Gen(float Weight)
    {
        this.Weight = Weight;
    }
    public Gen(Gen gen)
    {
        ElementaryNeuronNumberInList = gen.ElementaryNeuronNumberInList;
        FinitieNeuronNumberInList = gen.FinitieNeuronNumberInList;
        Weight = gen.Weight;
    }
    public void BindToNeurons(List<Neuron> neurons)
    {
        ElementaryNeuron = neurons[ElementaryNeuronNumberInList];
        FinitieNeuron = neurons[FinitieNeuronNumberInList];
    }
}