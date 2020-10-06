using System;
using System.Collections.Generic;
using System.Linq;
using DWC.Blazor.Extensions;
using Xunit;

namespace DWC.Blazor.Tests
{
    public class ShuffleAlgorithmTests
    {
        [Fact]
        public void Shuffle_NullSource_Should_ThrowsException()
        {
            // Arrange
            List<int> source = null;

            // Act
            Action action = () => source.Shuffle().ToList();

            // Assert
            Assert.Throws<ArgumentNullException>(action);
        }

        [Fact]
        public void Shuffle_NullRandom_Should_ThrowsException()
        {
            // Arrange
            var source = new List<int> { 1, 2, 3 };
            Random rand = null;

            // Act
            Action action = () => source.Shuffle(rand).ToList();

            // Assert
            Assert.Throws<ArgumentNullException>(action);
        }

        [Fact]
        public void Shuffle_SourceOk_Should_ShuffleList()
        {
            // Arrange
            var source = Enumerable.Range(0, 50);

            // Act
            var result = source.Shuffle().ToList();

            // Assert
            Assert.NotEqual<int>(source, result);
        }
    }
}
