using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XMPS2000.Core.Devices.Slaves;

namespace XMPS2000.UndoRedoGridLayout
{
    public class PublishManager
    {
        public static List<Publish> PublishList { get; private set; } = new List<Publish>();
        private static Stack<List<Publish>> undoStack = new Stack<List<Publish>>();
        private static Stack<List<Publish>> redoStack = new Stack<List<Publish>>();
        public PublishManager()
        {
        }

        public PublishManager(List<Publish> initialList)
        {
            undoStack.Clear();
            redoStack.Clear();
            PublishList = initialList.Select(p => new Publish
            {
                PubRequest = p.PubRequest,
                topic = p.topic,
                retainflag = p.retainflag,
                qos = p.qos,
                keyvalue = p.keyvalue
            }).ToList();
        }
        public void SaveState()
        {
            undoStack.Push(PublishList.Select(p => new Publish
            {
                topic = p.topic,
                keyvalue = p.keyvalue,
                qos = p.qos,
                retainflag = p.retainflag,
                PubRequest = p.PubRequest.Select(r => new PubRequest
                {
                    Keyvalue = r.Keyvalue,
                    req = r.req,
                    Tag = r.Tag
                }).ToList()
            }).ToList());
            redoStack.Clear();
        }
        public void AddPublish(Publish publish)
        {
            SaveState();
            PublishList.Add(publish);
        }

        public void DeletePublish(Publish publish)
        {
            SaveState();
            PublishList.Remove(publish);
        }
        public void UpdatePublish(Publish oldPublish, Publish updatedPublish)
        {
            SaveState();
            var existing = PublishList.FirstOrDefault(p => p.keyvalue == oldPublish.keyvalue);
            if (existing != null)
            {
                existing.topic = updatedPublish.topic;
                existing.keyvalue = updatedPublish.keyvalue;
                existing.qos = updatedPublish.qos;
                existing.retainflag = updatedPublish.retainflag;
                existing.PubRequest = new List<PubRequest>(updatedPublish.PubRequest.Select(r => new PubRequest
                {
                    Keyvalue = r.Keyvalue,
                    req = r.req,
                    Tag = r.Tag
                }));
            }
        }

        public void UpdateOnlyForRequest(Publish obj)
        {
            var existing = PublishList.FirstOrDefault(p => p.keyvalue == obj.keyvalue);
            existing.PubRequest = obj.PubRequest;
        }
        public List<Publish> Undo()
        {
            if (undoStack.Count > 0)
            {
                redoStack.Push(PublishList.Select(p => new Publish
                {
                    topic = p.topic,
                    keyvalue = p.keyvalue,
                    qos = p.qos,
                    retainflag = p.retainflag,
                    PubRequest = p.PubRequest,
                }).ToList());

                PublishList = undoStack.Pop()
                    .Select(p => new Publish
                    {
                        topic = p.topic,
                        keyvalue = p.keyvalue,
                        qos = p.qos,
                        retainflag = p.retainflag,
                        PubRequest = p.PubRequest.Select(r => new PubRequest
                        {
                            Keyvalue = r.Keyvalue,
                            req = r.req,
                            Tag = r.Tag
                        }).ToList()
                    }).ToList();

                return PublishList;
            }
            return null;
        }
        public List<Publish> Redo()
        {
            if (redoStack.Count > 0)
            {
                undoStack.Push(PublishList.Select(p => new Publish
                {
                    topic = p.topic,
                    keyvalue = p.keyvalue,
                    qos = p.qos,
                    retainflag = p.retainflag,
                    PubRequest = p.PubRequest.Select(r => new PubRequest
                    {
                        Keyvalue = r.Keyvalue,
                        req = r.req,
                        Tag = r.Tag
                    }).ToList()
                }).ToList());

                PublishList = redoStack.Pop()
                    .Select(p => new Publish
                    {
                        topic = p.topic,
                        keyvalue = p.keyvalue,
                        qos = p.qos,
                        retainflag = p.retainflag,
                        PubRequest = p.PubRequest.Select(r => new PubRequest
                        {
                            Keyvalue = r.Keyvalue,
                            req = r.req,
                            Tag = r.Tag
                        }).ToList()
                    }).ToList();

                return PublishList;
            }
            return null;
        }
    }
}
