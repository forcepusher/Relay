using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace BananaParty.WebSocketRelay.Tests
{
    public class ArrayStateIntegrationTests
    {
        [Test]
        public void ShouldRoundTripStaticArrayJson()
        {
            var source = new List<IntegerState>
            {
                new IntegerState("Val1", 10),
                new IntegerState("Val2", 20)
            };
            var target = new List<IntegerState>
            {
                new IntegerState("Val1", 0),
                new IntegerState("Val2", 0)
            };

            var sourceState = new StaticArrayState<IntegerState>("Array", source);
            var targetState = new StaticArrayState<IntegerState>("Array", target);
            var output = new JsonStateOutput(prettyPrint: false, bracesOnNewLine: false);

            new ObjectState("Root", new List<IState> { sourceState }).WriteState(output);
            new ObjectState("Root", new List<IState> { targetState }).ReadState(new JsonStateInput(output.ToString()));

            Assert.AreEqual(10, target[0].Value);
            Assert.AreEqual(20, target[1].Value);
        }

        [Test]
        public void ShouldRoundTripStaticArrayBinary()
        {
            var source = new List<IntegerState>
            {
                new IntegerState("Val1", 10),
                new IntegerState("Val2", 20)
            };
            var target = new List<IntegerState>
            {
                new IntegerState("Val1", 0),
                new IntegerState("Val2", 0)
            };

            var sourceState = new StaticArrayState<IntegerState>("Array", source);
            var targetState = new StaticArrayState<IntegerState>("Array", target);
            var output = new BinaryStateOutput();

            new ObjectState("Root", new List<IState> { sourceState }).WriteState(output);
            new ObjectState("Root", new List<IState> { targetState }).ReadState(new BinaryStateInput(output.GetBuffer()));

            Assert.AreEqual(10, target[0].Value);
            Assert.AreEqual(20, target[1].Value);
        }

        [Test]
        public void ShouldThrowWhenStaticArrayLengthMismatchesJson()
        {
            var source = new List<IntegerState> { new IntegerState("Val1", 10) };
            var target = new List<IntegerState> { new IntegerState("Val1", 0), new IntegerState("Val2", 0) };

            var sourceState = new StaticArrayState<IntegerState>("Array", source);
            var targetState = new StaticArrayState<IntegerState>("Array", target);
            var output = new JsonStateOutput(prettyPrint: false, bracesOnNewLine: false);

            new ObjectState("Root", new List<IState> { sourceState }).WriteState(output);

            // The JSON for a static array just writes the elements.
            // If target expects 2 but only gets 1, it should fail or handle accordingly.
            Assert.Throws<InvalidOperationException>(() =>
            {
                new ObjectState("Root", new List<IState> { targetState }).ReadState(new JsonStateInput(output.ToString()));
            });
        }
    }
}
