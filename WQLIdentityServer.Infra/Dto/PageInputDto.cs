namespace WQLIdentityServer.Infra.Dto
{
    /// <summary>
    /// 分页输入
    /// </summary>
    public class PageInputDto
    {
        public string Search { get; set; }
        public bool Isdesc { get; set; } = true;
        private int page;

        /// <summary>
        /// 页码
        /// </summary>
        public int Page
        {
            get
            {
                return page;
            }
            set
            {
                page = value;
                if (page <= 0) page = 1;

            }
        }

        /// <summary>
        /// 每页大小
        /// </summary>
        public int PageSize
        {
            get; set;
        }
    }
}
