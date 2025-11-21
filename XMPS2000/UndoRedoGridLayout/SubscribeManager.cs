using System.Collections.Generic;
using System.Linq;
using XMPS2000.Core.Devices.Slaves;

namespace XMPS2000.UndoRedoGridLayout
{
    internal class SubscribeManager
    {
        public static List<Subscribe> SubscribeList { get; private set; } = new List<Subscribe>();
        private static Stack<List<Subscribe>> undoStack = new Stack<List<Subscribe>>();
        private static Stack<List<Subscribe>> redoStack = new Stack<List<Subscribe>>();
        public SubscribeManager()
        {
        }

        public SubscribeManager(List<Subscribe> initialList)
        {
            undoStack.Clear();
            redoStack.Clear();
            SubscribeList = initialList.Select(p => new Subscribe
            {
                SubRequest = p.SubRequest,
                topic = p.topic,
                qos = p.qos,
                key = p.key
            }).ToList();
        }
        public void SaveState()
        {
            undoStack.Push(SubscribeList.Select(p => new Subscribe
            {
                topic = p.topic,
                key = p.key,
                qos = p.qos,
                SubRequest = p.SubRequest.Select(r => new SubscribeRequest
                {
                    key = r.key,
                    req = r.req,
                    Tag = r.Tag
                }).ToList()
            }).ToList());
            redoStack.Clear();
        }
        public void AddSubscribe(Subscribe subscribe)
        {
            SaveState();
            SubscribeList.Add(subscribe);
        }

        public void DeleteSubscribe(Subscribe subscribe)
        {
            SaveState();
            SubscribeList.Remove(subscribe);
        }
        public void UpdateSubscribe(Subscribe oldSubcribe, Subscribe updatedSubscribe)
        {
            SaveState();
            var existing = SubscribeList.FirstOrDefault(p => p.key == oldSubcribe.key);
            if (existing != null)
            {
                existing.topic = updatedSubscribe.topic;
                existing.key = updatedSubscribe.key;
                existing.qos = updatedSubscribe.qos;
                existing.SubRequest = new List<SubscribeRequest>(updatedSubscribe.SubRequest.Select(r => new SubscribeRequest
                {
                    key = r.key,
                    req = r.req,
                    Tag = r.Tag
                }));
            }
        }
        public void UpdateOnlyForRequest(Subscribe obj)
        {
            var existing = SubscribeList.FirstOrDefault(p => p.key == obj.key);
            existing.SubRequest = obj.SubRequest;
        }
        public List<Subscribe> Undo()
        {
            if (undoStack.Count > 0)
            {
                redoStack.Push(SubscribeList.Select(p => new Subscribe
                {
                    topic = p.topic,
                    key = p.key,
                    qos = p.qos,
                    SubRequest = p.SubRequest,
                }).ToList());

                SubscribeList = undoStack.Pop()
                    .Select(p => new Subscribe
                    {
                        topic = p.topic,
                        key = p.key,
                        qos = p.qos,
                        SubRequest = p.SubRequest.Select(r => new SubscribeRequest
                        {
                            key = r.key,
                            req = r.req,
                            Tag = r.Tag
                        }).ToList()
                    }).ToList();

                return SubscribeList;
            }
            return null;
        }
        public List<Subscribe> Redo()
        {
            if (redoStack.Count > 0)
            {
                undoStack.Push(SubscribeList.Select(p => new Subscribe
                {
                    topic = p.topic,
                    key = p.key,
                    qos = p.qos,
                    SubRequest = p.SubRequest.Select(r => new SubscribeRequest
                    {
                        key = r.key,
                        req = r.req,
                        Tag = r.Tag
                    }).ToList()
                }).ToList());

                SubscribeList = redoStack.Pop()
                    .Select(p => new Subscribe
                    {
                        topic = p.topic,
                        key = p.key,
                        qos = p.qos,
                        SubRequest = p.SubRequest.Select(r => new SubscribeRequest
                        {
                            key = r.key,
                            req = r.req,
                            Tag = r.Tag
                        }).ToList()
                    }).ToList();

                return SubscribeList;
            }
            return null;
        }
    }
}
