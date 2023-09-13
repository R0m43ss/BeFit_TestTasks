using System.Collections.Generic;
using System.Linq;

namespace BeFit_TestTasks
{
    //Есть N чиновников, каждый из которых выдает справку определенного вида.Кроме того, у каждого чиновника есть набор справок, которые нужно получить до того, как обратиться к нему за справкой.Запрограммировать алгоритм, по которому можно получить все справки.
    //Пример: N = 4. Зависимость между чиновниками – { 1,[2]},{2,[3,4]} (т.е первый чиновник чтобы дать справку требует справку от второго, а второй от третьего и четвертого).
    //Допустимые ответы:
    //{ 3, 4, 2, 1}
    //{ 4, 3, 2, 1}
    public static class GetDocumentTask
    {
        private static readonly DocumentStorage _storage = new DocumentStorage();

        public static void GetAllDocuments(User userRequested)
        {
            foreach (var documentId in _storage.AllDocumentIds)
            {
                if (userRequested.ReceivedDocuments.Any(document => document.Id == documentId)) return;

                GetRequiredDocument(documentId, userRequested);
            }
        }

        private static void GetRequiredDocument(int documentId, User userRequested, int depth = 1)
        {
            if (depth > _storage.AllDocumentIds.Count) return;
            if (!_storage.IsDocumentExists(documentId)) return;

            var requiredDocumentIds = _storage.GetDocumentRequirements(documentId);
            foreach (var requiredDocumentId in requiredDocumentIds)
            {
                if (requiredDocumentId == documentId) continue;

                GetRequiredDocument(requiredDocumentId, userRequested, depth + 1);
            }

            var document = _storage.TryGetDocument(documentId, userRequested);
            if (document != null) userRequested.ReceivedDocuments.Add(document);
        }
    }

    /// <summary>
    /// Здесь идет вся работа со справками
    /// </summary>
    public class DocumentStorage
    {
        private readonly IReadOnlyCollection<Document> _documents;

        public DocumentStorage()
        {
            _documents = new[] {
                new Document(1, new [] { 2 }),
                new Document(2, new [] { 3, 4 }),
                new Document(3),
                new Document(4),
            };
        }

        /// <summary>
        /// Список справок, которые можно получить
        /// </summary>
        public IReadOnlyCollection<int> AllDocumentIds => _documents.Select(document => document.Id).ToList();

        /// <summary>
        /// Проверка, выдают ли вообще нужную справку
        /// </summary>
        /// <param name="documentId"></param>
        /// <returns></returns>
        public bool IsDocumentExists(int documentId)
        {
            return _documents.Any(document => document.Id == documentId);
        }

        /// <summary>
        /// Проверка возможности получения справки и выдача в случае успеха
        /// </summary>
        /// <param name="documentId"></param>
        /// <param name="userRequested"></param>
        /// <returns></returns>
        public Document TryGetDocument(int documentId, User userRequested)
        {
            if (!IsDocumentExists(documentId)) return null;

            var foundDocument = _documents.First(document => document.Id == documentId);
            return foundDocument.IsAvailableForUser(userRequested) ? foundDocument : null;
        }

        /// <summary>
        /// Список требований для получения справки
        /// </summary>
        /// <param name="documentId"></param>
        /// <returns></returns>
        public IEnumerable<int> GetDocumentRequirements(int documentId)
        {
            if (!IsDocumentExists(documentId)) return Enumerable.Empty<int>();

            var foundDocument = _documents.First(document => document.Id == documentId);
            return foundDocument.RequiredDocumentIds;
        }
    }
    public class Document
    {
        public int Id { get; set; }

        public IEnumerable<int> RequiredDocumentIds { get; }

        public Document(int id, IEnumerable<int> requiredDocumentIds = null)
        {
            Id = id;
            RequiredDocumentIds = requiredDocumentIds ?? Enumerable.Empty<int>();
        }

        /// <summary>
        /// Проверка требований для получения справки
        /// </summary>
        /// <param name="userRequested"></param>
        /// <returns></returns>
        public bool IsAvailableForUser(User userRequested)
        {
            if (RequiredDocumentIds.Count() == 0) return true;

            return RequiredDocumentIds.All(id => userRequested.ReceivedDocuments.Any(document => document.Id == id));
        }
    }

    public class User
    {
        public int Id { get; set; }
        public List<Document> ReceivedDocuments { get; set; }

        public User() 
        {
            ReceivedDocuments = new List<Document>();
        }

        public static readonly User System = new User() { Id = -1 };
    }
}
