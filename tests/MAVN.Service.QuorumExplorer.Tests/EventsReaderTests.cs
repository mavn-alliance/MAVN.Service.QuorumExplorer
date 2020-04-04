using System;
using System.Threading.Tasks;
using MAVN.Service.QuorumExplorer.Domain.DTOs;
using MAVN.Service.QuorumExplorer.Domain.Repositories;
using MAVN.Service.QuorumExplorer.Domain.Services.Explorer;
using MAVN.Service.QuorumExplorer.DomainServices.Explorer;
using Moq;
using Xunit;

namespace MAVN.Service.QuorumExplorer.Tests
{
    public class EventsReaderTests
    {
        private const string ValidBlockHash = "0x1eb034108ab3dc4a16bc37770b237467aef2823ac57fcacd27bcca39ec15adc3";

        private const string ValidTransactionHash = "0x0006a68ae98bf15a6d0f22eb2305c9b0ae9c265a6013023f3b25afa1211d1a66";
        
        private readonly Mock<IEventRepository> _eventRepositoryMock = new Mock<IEventRepository>();
        
        [Theory]
        [InlineData(-1, 1, 10)]
        [InlineData(0, 0, 10)]
        [InlineData(0, 1, 0)]
        [InlineData(-1, 0, 0)]
        public async Task GetByBlockAsync_Number_InvalidInputParameters_RaisesException(
            long blockNumber, 
            int currentPage, 
            int pageSize)
        {
            var sut = CreateSutInstance();

            await Assert.ThrowsAsync<ArgumentException>(() => sut.GetByBlockAsync(blockNumber, currentPage, pageSize));
        }

        [Theory]
        [InlineData("", 1, 10)]
        [InlineData(null, 1, 10)]
        [InlineData(ValidBlockHash, 0, 10)]
        [InlineData(ValidBlockHash, 1, 0)]
        [InlineData("", 0, 0)]
        [InlineData(null, 0, 0)]
        public async Task GetByBlockAsync_Hash_InvalidInputParameters_RaisesException(
            string blockHash, 
            int currentPage,
            int pageSize)
        {
            var sut = CreateSutInstance();
            
            await Assert.ThrowsAsync<ArgumentException>(() => sut.GetByBlockAsync(blockHash, currentPage, pageSize));
        }

        [Theory]
        [InlineData("", 1, 10)]
        [InlineData(null, 1, 10)]
        [InlineData(ValidTransactionHash, 0, 10)]
        [InlineData(ValidTransactionHash, 1, 0)]
        [InlineData("", 0, 0)]
        [InlineData(null, 0, 0)]
        public async Task GetByTransactionAsync_InvalidInputParameters_RaisesException(string txHash, int currentPage, int pageSize)
        {
            var sut = CreateSutInstance();

            await Assert.ThrowsAsync<ArgumentException>(() => sut.GetByTransactionAsync(txHash, currentPage, pageSize));
        }

        [Theory]
        [InlineData(0, 10)]
        [InlineData(1, 0)]
        [InlineData(0, 0)]
        public async Task GetFilteredAsync_InvalidFilter_RaisesException(int currentPage, int pageSize)
        {
            var sut = CreateSutInstance();

            await Assert.ThrowsAsync<ArgumentNullException>(() => sut.GetFilteredAsync(null, currentPage, pageSize));
        }

        [Theory]
        [InlineData(0, 10)]
        [InlineData(1, 0)]
        [InlineData(0, 0)]
        public async Task GetFilteredAsync_InvalidPageInformation_RaisesException(int currentPage, int pageSize)
        {
            var sut = CreateSutInstance();

            await Assert.ThrowsAsync<ArgumentException>(() => sut.GetFilteredAsync(new EventsFilter(), currentPage, pageSize));
        }

        private IEventsReader CreateSutInstance()
        {
            return new EventsReader(_eventRepositoryMock.Object);
        }
    }
}
