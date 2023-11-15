namespace CongestionTaxCalculator.Core.Exceptions
{
    public class RecordNotFoundException : Exception
    {
        public RecordNotFoundException(int id , Type recordType)
        {
            Id = id;
            RecordType = recordType;
        }

        public int Id { get; private set; }
        public Type RecordType { get;private set; }

        public override string ToString()
        {
            return $"{RecordType.Name} record with id {Id} not found!";
        }
    }
}
