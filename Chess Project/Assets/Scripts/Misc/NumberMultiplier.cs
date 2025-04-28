using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum OperationTypes {
    PreAdd, PostAdd, Multiply, Ignore
}
public struct Operation {
    public OperationTypes type; public int amount;

    public Operation(OperationTypes type, int amount) {
        this.type = type; this.amount = amount;
    }
}

public class NumberMultiplier
{
    int val;
    List<Operation> operations;

    public NumberMultiplier(int val) {
        this.val = val;
    }


    public void addOperation(OperationTypes type, int amount) {
        if(type != OperationTypes.Ignore) {
            this.operations.Add(new Operation(type,amount));
        }
    }


    public int resolve() {
        int res = val;
        foreach (Operation operation in operations) {
            if(operation.type == OperationTypes.PreAdd) {
                res += operation.amount;
            }
        }

        int multiplier = 100;
        foreach (Operation operation in operations) {
            if(operation.type == OperationTypes.Multiply) {
                multiplier += operation.amount;
            }
        }

        res = (int)(res * multiplier/100.0);
        foreach (Operation operation in operations) {
            if(operation.type == OperationTypes.PostAdd) {
                res += operation.amount;
            }
        }

        return res;
    }
}
