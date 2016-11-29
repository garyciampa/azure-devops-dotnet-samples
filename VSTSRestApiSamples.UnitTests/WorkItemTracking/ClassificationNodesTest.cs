﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using VstsRestApiSamples.WorkItemTracking;
using VstsRestApiSamples.ViewModels.WorkItemTracking;
using System.Net;

namespace VstsRestApiSamples.Tests.WorkItemTracking
{
    [TestClass]
    public class ClassificationNodesTest
    {
        private IConfiguration _configuration = new Configuration();

        [TestInitialize]
        public void TestInitialize()
        {
            InitHelper.GetConfiguration(_configuration);
        }

        [TestCleanup]
        public void TestCleanup()
        {
            _configuration = null;
        }

        [TestMethod, TestCategory("REST API")]
        public void WorkItemTracking_Nodes_GetAreas_Success()
        {
            // arrange
            ClassificationNodes request = new ClassificationNodes(_configuration);

            // act
            ListOfNodesResponse.Nodes response = request.GetAreas(_configuration.Project);

            //assert
            Assert.AreEqual(HttpStatusCode.OK, response.HttpStatusCode);
           
            request = null;
        }

        [TestMethod, TestCategory("REST API")]
        public void WorkItemTracking_Nodes_GetIterations_Success()
        {
            // arrange
            ClassificationNodes request = new ClassificationNodes(_configuration);

            // act
            ListOfNodesResponse.Nodes response = request.GetIterations(_configuration.Project);

            //assert
            Assert.AreEqual(HttpStatusCode.OK, response.HttpStatusCode);

            request = null;
        }

        [TestMethod, TestCategory("REST API")]
        public void WorkItemTracking_Nodes_CreateIteration_Success()
        {
            // arrange
            ClassificationNodes request = new ClassificationNodes(_configuration);
            DateTime startDate = new DateTime(2016, 11, 28);
            DateTime finishDate = new DateTime(2016, 12, 16);
            string path = "Iteration Foo";

            // act
            GetNodeResponse.Node response = request.CreateIteration(_configuration.Project, path, startDate, finishDate);

            //assert
            if (response.Message.Contains("VS402371: Classification node name " + path))
            {
                Assert.Inconclusive("Area path '" + path + "' already exists");
            }
            else
            {
                Assert.AreEqual(HttpStatusCode.Created, response.HttpStatusCode);
            }
            request = null;
        }

        [TestMethod, TestCategory("REST API")]
        public void WorkItemTracking_Nodes_UpdateIterationDates_Success()
        {
            // arrange
            ClassificationNodes request = new ClassificationNodes(_configuration);
            DateTime startDate = new DateTime(2016, 11, 29);
            DateTime finishDate = new DateTime(2016, 12, 17);
            string path = "Iteration Foo";

            // act
            GetNodeResponse.Node response = request.UpdateIterationDates(_configuration.Project, path, startDate, finishDate);

            //assert
            Assert.AreEqual(HttpStatusCode.OK, response.HttpStatusCode);

            request = null;
        }
    }
}
