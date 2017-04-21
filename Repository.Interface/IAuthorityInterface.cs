using System;
using System.Collections.Generic;
using SystemCenter;

namespace Repository.Interface
{
    public interface IAuthorityInterface
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        AUTHORITY GetOne(int id);
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        IEnumerable<AUTHORITY> GetAll();
        /// <summary>
        /// For Intranet
        /// </summary>
        /// <param name="UserId"></param>
        /// <returns></returns>
        //IEnumerable<AUTHORITY> UserAuthorization(string UserId);
        /// <summary>
        /// For single application
        /// </summary>
        /// <param name="UserId"></param>
        /// <param name="SystemId"></param>
        /// <returns></returns>
        //IEnumerable<AUTHORITY> UserAuthorization(string UserId, string SystemId);

        /// <summary>
        /// 讀取功能選單
        /// </summary>
        /// <returns></returns>
        List<FUNCTION> GetMenu(string USR_ID);

    }
}
