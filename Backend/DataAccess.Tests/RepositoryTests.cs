using System;
using System.Linq;
using DomainModels;
using IDataAccess;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DataAccess.Tests
{
    [TestClass]
    public class RepositoryTests
    {
        private DownloadedFile downloadedFile;
        IRepository<DownloadedFile> repo;
        [TestInitialize]
        public void TestInitialize()
        {

            repo = new Repository<DownloadedFile>(new UnitOfWork());
            // insert test data
            downloadedFile = new DownloadedFile
            {
                FileRemotePath = "ftp://host/file",
                LocalPath = "/temp",
                ProcessingStatus = 1
            };

        }

        [TestCleanup]
        public void TestCleanUp()
        {
            repo.Delete(downloadedFile);
            repo.Dispose();
        }


        //IQueryable<T> GetAll();
    //T Find(int id);
    //T Add(T entity);
    //void Update(T entity);
        //void Delete(T entity);
        //void SaveChanges();

        [TestMethod]
        public void Add_ValidEntity_SaveSucess()
        {
            //arrange

            //act
            repo.Add(downloadedFile);
            repo.SaveChanges();

            //assert
            Assert.IsTrue(downloadedFile.Id > 0);

        }

        [TestMethod]
        public void Find_SearchForEntity_ReturnedSucess()
        {
            //arrange
            repo.Add(downloadedFile);
            repo.SaveChanges();

            //act
            var result = repo.Find(downloadedFile.Id);

            //assert
            Assert.AreEqual(downloadedFile.Id, result.Id);

        }

        [TestMethod]
        public void Update_UpdateProcessingStatus_UpdateSucess()
        {
            //arrange
            repo.Add(downloadedFile);
            repo.SaveChanges();

            //act
            var newStatus = 2;

            var TempFile = repo.Find(downloadedFile.Id);
            TempFile.ProcessingStatus = newStatus;

            repo.Update(TempFile);

            //assert
            Assert.AreEqual(downloadedFile.ProcessingStatus, newStatus);

        }

        [TestMethod]
        public void Delete_DeleteEntity_DeleteSucess()
        {
            //arrange
            repo.Add(downloadedFile);
            repo.SaveChanges();
            var id = downloadedFile.Id;
            //act

            var TempFile = repo.Find(downloadedFile.Id);
            repo.Delete(TempFile);
            repo.SaveChanges();

            var result = repo.GetAll().Any(o => o.Id == id);
            //assert
            Assert.AreEqual(result,false);

        }




    }
}
