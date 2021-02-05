namespace Market.Application.Models
{
    public abstract class GridFilterViewModel
    {
        private int _pageLength;
        private int _pageNumber;


        /// <summary>
        /// Number of records to show in a page. Default value is 15
        /// </summary>
        public int PageLength
        {
            get => _pageLength < 1 ? 15 : _pageLength;
            set
            {
                if (value < 1)
                {
                    value = 15;
                }
                _pageLength = value;
            }
        }



        /// <summary>
        /// Number of page to see. Initially starts from 1
        /// </summary>
        public int PageNumber
        {
            get => _pageNumber < 1 ? 1 : _pageNumber;
            set
            {
                if (value < 1)
                {
                    value = 1;
                }
                _pageNumber = value;
            }
        }
    }
}
