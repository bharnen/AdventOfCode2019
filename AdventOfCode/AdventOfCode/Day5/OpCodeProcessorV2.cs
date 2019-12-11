using AdventOfCode.Day2;
using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode.Day5
{
    class OpCodeProcessorV2 : OpCodeProcessor
    {
        public OpCodeProcessorV2() { }


        const int INPUT = 3;
        const int OUTPUT = 4;
        const int JUMP_IF_TRUE = 5;
        const int JUMP_IF_FALSE = 6;
        const int LESS_THAN = 7;
        const int EQUALS = 8;

        const int POSITION_MODE = 0;
        const int IMMEDIATE_MODE = 1;

        protected override void _processCode(List<int> opCodes, int[] instructionPointer, int fullCode)
        {
            var code = _getOpCode(fullCode);
            var parameterModes = _getParamModes(fullCode);
            switch (code)
            {
                case ADD:
                    _add(opCodes, instructionPointer[0], parameterModes);
                    instructionPointer[0] += 4;
                    return;
                case MULTIPLY:
                    _multiply(opCodes, instructionPointer[0], parameterModes);
                    instructionPointer[0] += 4;
                    return;
                case INPUT:
                    _handleInput(opCodes, instructionPointer[0]);
                    instructionPointer[0] += 2;
                    return;
                case OUTPUT:
                    _handleOutput(opCodes, instructionPointer[0], parameterModes);
                    instructionPointer[0] += 2;
                    return;
                case JUMP_IF_TRUE:
                    if (!_jumpIfTrue(opCodes, instructionPointer, parameterModes))
                    {
                        instructionPointer[0] += 3;
                    }
                    return;
                case JUMP_IF_FALSE:
                    if (!_jumpIfFalse(opCodes, instructionPointer, parameterModes))
                    {
                        instructionPointer[0] += 3;
                    }
                    return;
                case LESS_THAN:
                    _lessThan(opCodes, instructionPointer, parameterModes);
                    instructionPointer[0] += 4;
                    return;
                case EQUALS:
                    _equals(opCodes, instructionPointer, parameterModes);
                    instructionPointer[0] += 4;
                    return;
                default:
                    return;
            }
        }

        private void _add(List<int> opCodes, int instructionPointer, List<int> paramModes)
        {
            var paramOne = _getParam(opCodes, instructionPointer, 1, paramModes.Count > 0 ? paramModes[0] : POSITION_MODE);
            var paramTwo = _getParam(opCodes, instructionPointer, 2, paramModes.Count > 1 ? paramModes[1] : POSITION_MODE);
            var paramThree = opCodes[instructionPointer + 3];
            if (paramOne == null || paramTwo == null) return;
            opCodes[paramThree] = paramOne.Value + paramTwo.Value;
        }
        private void _multiply(List<int> opCodes, int instructionPointer, List<int> paramModes)
        {
            var paramOne = _getParam(opCodes, instructionPointer, 1, paramModes.Count > 0 ? paramModes[0] : POSITION_MODE);
            var paramTwo = _getParam(opCodes, instructionPointer, 2, paramModes.Count > 1 ? paramModes[1] : POSITION_MODE);
            var paramThree = opCodes[instructionPointer + 3];
            if (paramOne == null || paramTwo == null)
            {
                return;
            }
            opCodes[paramThree] = paramOne.Value * paramTwo.Value;
        }
        private bool _jumpIfTrue(List<int> opCodes, int[] instructionPointer, List<int> paramModes)
        {
            var paramOne = _getParam(opCodes, instructionPointer[0], 1, paramModes.Count > 0 ? paramModes[0] : POSITION_MODE);
            if (paramOne != 0)
            {
                var paramTwo = _getParam(opCodes, instructionPointer[0], 2, paramModes.Count > 1 ? paramModes[1] : POSITION_MODE);
                instructionPointer[0] = paramTwo.Value;
                return true;
            }
            return false;
        }
        private bool _jumpIfFalse(List<int> opCodes, int[] instructionPointer, List<int> paramModes)
        {
            var paramOne = _getParam(opCodes, instructionPointer[0], 1, paramModes.Count > 0 ? paramModes[0] : POSITION_MODE);
            if (paramOne == 0)
            {
                var paramTwo = _getParam(opCodes, instructionPointer[0], 2, paramModes.Count > 1 ? paramModes[1] : POSITION_MODE);
                instructionPointer[0] = paramTwo.Value;
                return true;
            }
            return false;
        }
        private bool _lessThan(List<int> opCodes, int[] instructionPointer, List<int> paramModes)
        {
            var paramOne = _getParam(opCodes, instructionPointer[0], 1, paramModes.Count > 0 ? paramModes[0] : POSITION_MODE);
            var paramTwo = _getParam(opCodes, instructionPointer[0], 2, paramModes.Count > 1 ? paramModes[1] : POSITION_MODE);
            var paramThree = _getParam(opCodes, instructionPointer[0], 3, IMMEDIATE_MODE);
            if (paramOne < paramTwo)
            {
                opCodes[paramThree.Value] = 1;
                return true;
            }
            else
            {
                opCodes[paramThree.Value] = 0;
            }
            return false;
        }
        private bool _equals(List<int> opCodes, int[] instructionPointer, List<int> paramModes)
        {
            var paramOne = _getParam(opCodes, instructionPointer[0], 1, paramModes.Count > 0 ? paramModes[0] : POSITION_MODE);
            var paramTwo = _getParam(opCodes, instructionPointer[0], 2, paramModes.Count > 1 ? paramModes[1] : POSITION_MODE);
            var paramThree = _getParam(opCodes, instructionPointer[0], 3, IMMEDIATE_MODE);
            if (paramOne == paramTwo)
            {
                opCodes[paramThree.Value] = 1;
                return true;
            }
            else
            {
                opCodes[paramThree.Value] = 0;
            }
            return false;
        }
        protected int? _getParam(List<int> opCodes, int instructionPointer, int index, int mode)
        {
            switch (mode)
            {
                case POSITION_MODE:
                    return opCodes[opCodes[instructionPointer + index]];
                case IMMEDIATE_MODE:
                    return opCodes[instructionPointer + index];
            }
            return null;
        }

        private void _handleInput(List<int> opCodes, int instructionPointer)
        {
            var resultLocation = opCodes[instructionPointer + 1];
            string val = Console.ReadLine();
            opCodes[resultLocation] = Int32.Parse(val);
        }
        private void _handleOutput(List<int> opCodes, int instructionPointer, List<int> paramModes)
        {
            var output = _getParam(opCodes, instructionPointer, 1, paramModes.Count > 0 ? paramModes[0] : POSITION_MODE);
            Console.WriteLine($"Output: {output}");
        }

        private int _getOpCode(int fullCode)
        {
            var codeAsString = fullCode.ToString();
            if (codeAsString.Length >= 2)
            {
                return Int32.Parse(codeAsString.Substring(codeAsString.Length - 2));
            }
            return fullCode;
        }

        private List<int> _getParamModes(int fullCode)
        {
            var paramModes = new List<int>();
            var codeAsArray = fullCode.ToString().ToCharArray();
            if (codeAsArray.Length > 2)
            {
                for (int i=codeAsArray.Length-3; i>=0; i--)
                {
                    var paramMode = Int32.Parse(codeAsArray[i].ToString());
                    paramModes.Add(paramMode);
                }
            }
            return paramModes;
        }
    }
}
