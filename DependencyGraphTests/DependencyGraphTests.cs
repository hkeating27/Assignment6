using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SpreadsheetUtilities;
namespace DevelopmentTests
{
    /// <summary>
    ///This is a test class for DependencyGraphTest and is intended
    ///to contain all DependencyGraphTest Unit Tests
    ///</summary>
    [TestClass()]
    public class DependencyGraphTest
    {
        /// <summary>
        /// Runs a simple test to determine if the HasDependents method
        /// works as intended. The first Assert should return true since "a'
        /// does have dependents. The second Assert should return false since
        /// "e" is not in the dependency graph and therefore has no dependents
        /// </summary>
        [TestMethod()]
        public void HasDependents()
        {
            DependencyGraph t = new DependencyGraph();
            t.AddDependency("a", "d");
            t.AddDependency("b", "c");

            Assert.IsTrue(t.HasDependents("a"));
            Assert.IsFalse(t.HasDependents("e"));
        }

        /// <summary>
        /// Runs a simple test to determine if the HasDependents method
        /// works as intended on an empty HashSet. The Assert should return
        /// false since the HashSet that corresponds to "a" has no items in it and
        /// thus "a" has no dependents.
        /// </summary>
        [TestMethod()]
        public void HasDependentsEmpty()
        {
            DependencyGraph t = new DependencyGraph();
            t.AddDependency("a", "d");
            t.AddDependency("b", "c");
            t.ReplaceDependents("a", new HashSet<string>());

            Assert.IsFalse(t.HasDependents("a"));
        }

        /// <summary>
        /// Runs a simple test to determine if the HasDependees method works
        /// as intended. The first Assert should return true since "d" does have
        /// dependees. The second Assert should return false since "a" has dependents, but
        /// not dependees.
        /// </summary>
        [TestMethod()]
        public void HasDependees()
        {
            DependencyGraph t = new DependencyGraph();
            t.AddDependency("a", "d");
            t.AddDependency("b", "c");

            Assert.IsTrue(t.HasDependees("d"));
            Assert.IsFalse(t.HasDependees("a"));
        }

        /// <summary>
        /// Runs a simple test to determine if the HasDependees works
        /// as intended on an Empty HashSet. The Assert should return false
        /// since the dependee HashSet that corresponds to "c" is empty and
        /// thus "c" has no dependees.
        /// </summary>
        [TestMethod()]
        public void HasDependeesEmpty()
        {
            DependencyGraph t = new DependencyGraph();
            t.AddDependency("a", "d");
            t.AddDependency("b", "c");
            t.ReplaceDependees("c", new HashSet<string>());

            Assert.IsFalse(t.HasDependees("c"));
        }

        /// <summary>
        /// Runs a simple test to determine if the this[] method works.
        /// The first Assert should equal 0 since "a" has dependents
        /// but does not have any dependees. The second Assert
        /// should equal 2 since "c" should have two dependees,
        /// "b" and "e".
        /// </summary>
        [TestMethod()]
        public void ThisTest()
        {
            DependencyGraph t = new DependencyGraph();
            t.AddDependency("a", "d");
            t.AddDependency("b", "c");
            t.AddDependency("e", "c");

            Assert.AreEqual(0, t["a"]);
            Assert.AreEqual(2, t["c"]);
        }

        /// <summary>
        /// Runs a complex test to determine if the this[] method works.
        /// The first Assert should return 2 since "d" should have 2
        /// dependees, "x" and "b". The second Assert should return 1
        /// since "c" should have 1 dependee, "z".
        /// </summary>
        [TestMethod()]
        public void ComplexThisTest()
        {
            DependencyGraph t = new DependencyGraph();
            t.AddDependency("a", "d");
            t.AddDependency("b", "c");
            t.AddDependency("e", "c");
            t.RemoveDependency("b", "c");
            t.ReplaceDependees("d", new HashSet<string>(){ "x", "b" });
            t.ReplaceDependees("c", new HashSet<string>() { "z" });

            Assert.AreEqual(2, t["d"]);
            Assert.AreEqual(1, t["c"]);
        }

        /// <summary>
        /// Runs a simple test to determine if the AddDependency
        /// method works as intended when trying to add the same
        /// dependency twice. The second Assert should return 3 since
        /// the fourth AddDependency should not add anything to the
        /// Dependency Graph.
        /// </summary>
        [TestMethod()]
        public void AddSameDependencyTest()
        {
            DependencyGraph t = new DependencyGraph();
            t.AddDependency("a", "d");
            t.AddDependency("b", "c");
            t.AddDependency("e", "c");
            Assert.AreEqual(3, t.Size);

            t.AddDependency("b", "c");
            Assert.AreEqual(3, t.Size);
        }

        /// <summary>
        /// Runs a simple test to determine if the RemoveDependency
        /// method works as intended when trying to remove a dependency 
        /// that is not in the Dependency Graph. The second Assert should
        /// return 3 since the call RemoveDependency should not remove
        /// anything from the Dependency Graph.
        /// </summary>
        [TestMethod()]
        public void RemoveNonExistentDependency()
        {
            DependencyGraph t = new DependencyGraph();
            t.AddDependency("a", "d");
            t.AddDependency("b", "c");
            t.AddDependency("e", "c");
            Assert.AreEqual(3, t.Size);

            t.RemoveDependency("x", "y");
            Assert.AreEqual(3, t.Size);
        }

        /// <summary>
        /// Runs a simple test to determine if the ReplaceDependents method
        /// updates the size of the Dependency Graph as intended. The second
        /// Assert should return 4 since ReplaceDependents should add one more
        /// dependency than was previously in the Dependency Graph.
        /// </summary>
        [TestMethod()]
        public void ReplaceDependantsCorrectSize()
        {
            DependencyGraph t = new DependencyGraph();
            t.AddDependency("a", "z");
            t.AddDependency("b", "x");
            t.AddDependency("c", "d");
            Assert.AreEqual(3, t.Size);

            t.ReplaceDependents("b", new HashSet<string>() { "z", "a" });
            Assert.AreEqual(4, t.Size);
        }

        /// <summary>
        /// Runs a simple test to determine if the ReplaceDependees method
        /// updates the size of the Dependency Graph as intended. The second
        /// Assert should return 4 since ReplaceDependees should add one more
        /// dependency than was previously in the Dependency Graph.
        /// </summary>
        [TestMethod()]
        public void ReplaceDependeesCorrectSize()
        {
            DependencyGraph t = new DependencyGraph();
            t.AddDependency("a", "z");
            t.AddDependency("b", "x");
            t.AddDependency("c", "d");
            Assert.AreEqual(3, t.Size);

            t.ReplaceDependees("d", new HashSet<string>() { "z", "a" });
            Assert.AreEqual(4, t.Size);
        }

        /// <summary>
        /// Runs a complex test to determine if the HasDependents and
        /// HasDependees methods works as intended. The first Assert should
        /// return true since "b" should have 1 dependent, "x". The second Assert 
        /// should return false since the dependency ("i", "j") was removed from the
        /// Dependency Graph, so "j" should have no dependees.
        /// </summary>
        [TestMethod()]
        public void ComplexHasDependeesDependents()
        {
            DependencyGraph t = new DependencyGraph();
            t.AddDependency("a", "x");
            t.AddDependency("x", "a");
            t.AddDependency("i", "j");
            t.ReplaceDependees("x", new HashSet<string>() { "b" });
            t.RemoveDependency("i", "j");

            Assert.IsTrue(t.HasDependents("b"));
            Assert.IsFalse(t.HasDependees("j"));
        }

        /// <summary>
        ///Empty graph should contain nothing
        ///</summary>
        [TestMethod()]
        public void SimpleEmptyTest()
        {
            DependencyGraph t = new DependencyGraph();
            Assert.AreEqual(0, t.Size);
        }

        /// <summary>
        ///Empty graph should contain nothing
        ///</summary>
        [TestMethod()]
        public void SimpleEmptyRemoveTest()
        {
            DependencyGraph t = new DependencyGraph();
            t.AddDependency("x", "y");
            Assert.AreEqual(1, t.Size);
            t.RemoveDependency("x", "y");
            Assert.AreEqual(0, t.Size);
        }

        /// <summary>
        ///Empty graph should contain nothing
        ///</summary>
        [TestMethod()]
        public void EmptyEnumeratorTest()
        {
            DependencyGraph t = new DependencyGraph();
            t.AddDependency("x", "y");
            IEnumerator<string> e1 = t.GetDependees("y").GetEnumerator();
            Assert.IsTrue(e1.MoveNext());
            Assert.AreEqual("x", e1.Current);
            IEnumerator<string> e2 = t.GetDependents("x").GetEnumerator();
            Assert.IsTrue(e2.MoveNext());
            Assert.AreEqual("y", e2.Current);
            t.RemoveDependency("x", "y");
            Assert.IsFalse(t.GetDependees("y").GetEnumerator().MoveNext());
            Assert.IsFalse(t.GetDependents("x").GetEnumerator().MoveNext());
        }

        /// <summary>
        ///Replace on an empty DG shouldn't fail
        ///</summary>
        [TestMethod()]
        public void SimpleReplaceTest()
        {
            DependencyGraph t = new DependencyGraph();
            t.AddDependency("x", "y");
            Assert.AreEqual(t.Size, 1);
            t.RemoveDependency("x", "y");
            t.ReplaceDependents("x", new HashSet<string>());
            t.ReplaceDependees("y", new HashSet<string>());
        }

        ///<summary>
        ///It should be possibe to have more than one DG at a time.
        ///</summary>
        [TestMethod()]
        public void StaticTest()
        {
            DependencyGraph t1 = new DependencyGraph();
            DependencyGraph t2 = new DependencyGraph();
            t1.AddDependency("x", "y");
            Assert.AreEqual(1, t1.Size);
            Assert.AreEqual(0, t2.Size);
        }

        /// <summary>
        ///Non-empty graph contains something
        ///</summary>
        [TestMethod()]
        public void SizeTest()
        {
            DependencyGraph t = new DependencyGraph();
            t.AddDependency("a", "b");
            t.AddDependency("a", "c");
            t.AddDependency("c", "b");
            t.AddDependency("b", "d");
            Assert.AreEqual(4, t.Size);
        }

        /// <summary>
        ///Non-empty graph contains something
        ///</summary>
        [TestMethod()]
        public void EnumeratorTest()
        {
            DependencyGraph t = new DependencyGraph();
            t.AddDependency("a", "b");
            t.AddDependency("a", "c");
            t.AddDependency("c", "b");
            t.AddDependency("b", "d");
            IEnumerator<string> e = t.GetDependees("a").GetEnumerator();
            Assert.IsFalse(e.MoveNext());
            e = t.GetDependees("b").GetEnumerator();
            Assert.IsTrue(e.MoveNext());
            String s1 = e.Current;
            Assert.IsTrue(e.MoveNext());
            String s2 = e.Current;
            Assert.IsFalse(e.MoveNext());
            Assert.IsTrue(((s1 == "a") && (s2 == "c")) || ((s1 == "c") && (s2 == "a")));
            e = t.GetDependees("c").GetEnumerator();
            Assert.IsTrue(e.MoveNext());
            Assert.AreEqual("a", e.Current);
            Assert.IsFalse(e.MoveNext());
            e = t.GetDependees("d").GetEnumerator();
            Assert.IsTrue(e.MoveNext());
            Assert.AreEqual("b", e.Current);
            Assert.IsFalse(e.MoveNext());
        }

        /// <summary>
        ///Non-empty graph contains something
        ///</summary>
        [TestMethod()]
        public void ReplaceThenEnumerate()
        {
            DependencyGraph t = new DependencyGraph();
            t.AddDependency("x", "b");
            t.AddDependency("a", "z");
            t.ReplaceDependents("b", new HashSet<string>());
            t.AddDependency("y", "b");
            t.ReplaceDependents("a", new HashSet<string>() { "c" });
            t.AddDependency("w", "d");
            t.ReplaceDependees("b", new HashSet<string>() { "a", "c" });
            t.ReplaceDependees("d", new HashSet<string>() { "b" });
            IEnumerator<string> e = t.GetDependees("a").GetEnumerator();
            Assert.IsFalse(e.MoveNext());
            e = t.GetDependees("b").GetEnumerator();
            Assert.IsTrue(e.MoveNext());
            String s1 = e.Current;
            Assert.IsTrue(e.MoveNext());
            String s2 = e.Current;
            Assert.IsFalse(e.MoveNext());
            Assert.IsTrue(((s1 == "a") && (s2 == "c")) || ((s1 == "c") && (s2 == "a")));
            e = t.GetDependees("c").GetEnumerator();
            Assert.IsTrue(e.MoveNext());
            Assert.AreEqual("a", e.Current);
            Assert.IsFalse(e.MoveNext());
            e = t.GetDependees("d").GetEnumerator();
            Assert.IsTrue(e.MoveNext());
            Assert.AreEqual("b", e.Current);
            Assert.IsFalse(e.MoveNext());
        }

        /// <summary>
        ///Using lots of data
        ///</summary>
        [TestMethod()]
        public void StressTest()
        {
            // Dependency graph
            DependencyGraph t = new DependencyGraph();
            // A bunch of strings to use
            const int SIZE = 200;
            string[] letters = new string[SIZE];
            for (int i = 0; i < SIZE; i++)
            {
                letters[i] = ("" + (char)('a' + i));
            }
            // The correct answers
            HashSet<string>[] dents = new HashSet<string>[SIZE];
            HashSet<string>[] dees = new HashSet<string>[SIZE];
            for (int i = 0; i < SIZE; i++)
            {
                dents[i] = new HashSet<string>();
                dees[i] = new HashSet<string>();
            }
            // Add a bunch of dependencies
            for (int i = 0; i < SIZE; i++)
            {
                for (int j = i + 1; j < SIZE; j++)
                {
                    t.AddDependency(letters[i], letters[j]);
                    dents[i].Add(letters[j]);
                    dees[j].Add(letters[i]);
                }
            }
            // Remove a bunch of dependencies
            for (int i = 0; i < SIZE; i++)
            {
                for (int j = i + 4; j < SIZE; j += 4)
                {
                    t.RemoveDependency(letters[i], letters[j]);
                    dents[i].Remove(letters[j]);
                    dees[j].Remove(letters[i]);
                }
            }
            // Add some back
            for (int i = 0; i < SIZE; i++)
            {
                for (int j = i + 1; j < SIZE; j += 2)
                {
                    t.AddDependency(letters[i], letters[j]);
                    dents[i].Add(letters[j]);
                    dees[j].Add(letters[i]);
                }
            }
            // Remove some more
            for (int i = 0; i < SIZE; i += 2)
            {
                for (int j = i + 3; j < SIZE; j += 3)
                {
                    t.RemoveDependency(letters[i], letters[j]);
                    dents[i].Remove(letters[j]);
                    dees[j].Remove(letters[i]);
                }
            }
            // Make sure everything is right
            for (int i = 0; i < SIZE; i++)
            {
                Assert.IsTrue(dents[i].SetEquals(new
        HashSet<string>(t.GetDependents(letters[i]))));
                Assert.IsTrue(dees[i].SetEquals(new
        HashSet<string>(t.GetDependees(letters[i]))));
            }
        }
    }
}