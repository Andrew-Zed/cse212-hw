using Microsoft.VisualStudio.TestTools.UnitTesting;

// TODO Problem 2 - Write and run test cases and fix the code to match requirements.

[TestClass]
public class PriorityQueueTests
{
    [TestMethod]
    // Scenario: Enqueue three items with increasing priorities (1, 3, 5) and dequeue all of them.
    // Expected Result: Items dequeued in descending priority order: "High", "Medium", "Low".
    // Defect(s) Found: Loop boundary `index < _queue.Count - 1` skipped the last item in the list; items where not removed from `_queue` on dequeue.
    public void TestPriorityQueue_1()
    {
        var priorityQueue = new PriorityQueue();
        priorityQueue.Enqueue("Low", 1);
        priorityQueue.Enqueue("Medium", 3);
        priorityQueue.Enqueue("High", 5);

        Assert.AreEqual("High", priorityQueue.Dequeue());
        Assert.AreEqual("Medium", priorityQueue.Dequeue());
        Assert.AreEqual("Low", priorityQueue.Dequeue());

    }

    [TestMethod]
    // Scenario: Enqueue multple items where two items share the highest priority. 
    // Expected Result: "FirstHigh" is dequeued before "SecondHigh" following FIFO order.
    // Defect(s) Found: Using `>=` caused the queue to overwrite the index with the later items of equal priority, violating FIFO order.
    public void TestPriorityQueue_2()
    {
        var priorityQueue = new PriorityQueue();
        priorityQueue.Enqueue("FirstHigh", 5);
        priorityQueue.Enqueue("Low", 2);
        priorityQueue.Enqueue("SecondHigh", 5);

        Assert.AreEqual("FirstHigh", priorityQueue.Dequeue());
        Assert.AreEqual("SecondHigh", priorityQueue.Dequeue());
        Assert.AreEqual("Low", priorityQueue.Dequeue());
    }

    // Add more test cases as needed below.

    [TestMethod]
    // Scenario: Attempt to dequeue from an empty PriorityQueue.
    // Expected Result: Throws an InvalidOperationException with message "The queue is empty."
    // Defect(s) Found: None. Empty check correctly throws exception.
    public void TestPriorityQueue_EmptyQueue()
    {
        var priorityQueue = new PriorityQueue();

        try
        {
            priorityQueue.Dequeue();
            Assert.Fail("Exception should have been thrown.");
        }
        catch (InvalidOperationException e)
        {
            Assert.AreEqual("The queue is empty.", e.Message);
        }
        catch (Exception e)
        {
            Assert.Fail($"Unexpected exception type: {e.GetType()}");
        }
    }

}