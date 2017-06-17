//数据类型 包装了两个基本类型 地图位置类
class Location
    {
        public int RIndex { get; set; }
        public int CIndex { get; set; }

        public Location (int rindex,int cindex)
        {
            this.RIndex = rindex;
            this.CIndex = cindex;
        }
    }
