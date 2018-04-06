using LightData.CMS.Controllers.Base;
using LightData.CMS.Modules.Library;
using System.IO;
using System.Web;
using System.Linq;
using System.Web.Mvc;
using System.Threading.Tasks;
using LightData.Auth.Helper;
using System.Collections.Generic;
using EntityWorker.Core.Helper;
using System;
using LightData.CMS.Modules.Helper;
using System.Text;

namespace LightData.CMS.Controllers
{
    public class FileUploaderController : BaseController
    {
        private const string jsIcon = "PD94bWwgdmVyc2lvbj0iMS4wIiBlbmNvZGluZz0iaXNvLTg4NTktMSI/Pgo8IS0tIEdlbmVyYXRvcjogQWRvYmUgSWxsdXN0cmF0b3IgMTguMC4wLCBTVkcgRXhwb3J0IFBsdWctSW4gLiBTVkcgVmVyc2lvbjogNi4wMCBCdWlsZCAwKSAgLS0+CjwhRE9DVFlQRSBzdmcgUFVCTElDICItLy9XM0MvL0RURCBTVkcgMS4xLy9FTiIgImh0dHA6Ly93d3cudzMub3JnL0dyYXBoaWNzL1NWRy8xLjEvRFREL3N2ZzExLmR0ZCI+CjxzdmcgeG1sbnM9Imh0dHA6Ly93d3cudzMub3JnLzIwMDAvc3ZnIiB4bWxuczp4bGluaz0iaHR0cDovL3d3dy53My5vcmcvMTk5OS94bGluayIgdmVyc2lvbj0iMS4xIiBpZD0iQ2FwYV8xIiB4PSIwcHgiIHk9IjBweCIgdmlld0JveD0iMCAwIDU2IDU2IiBzdHlsZT0iZW5hYmxlLWJhY2tncm91bmQ6bmV3IDAgMCA1NiA1NjsiIHhtbDpzcGFjZT0icHJlc2VydmUiIHdpZHRoPSI1MTJweCIgaGVpZ2h0PSI1MTJweCI+CjxnPgoJPHBhdGggc3R5bGU9ImZpbGw6I0U5RTlFMDsiIGQ9Ik0zNi45ODUsMEg3Ljk2M0M3LjE1NSwwLDYuNSwwLjY1NSw2LjUsMS45MjZWNTVjMCwwLjM0NSwwLjY1NSwxLDEuNDYzLDFoNDAuMDc0ICAgYzAuODA4LDAsMS40NjMtMC42NTUsMS40NjMtMVYxMi45NzhjMC0wLjY5Ni0wLjA5My0wLjkyLTAuMjU3LTEuMDg1TDM3LjYwNywwLjI1N0MzNy40NDIsMC4wOTMsMzcuMjE4LDAsMzYuOTg1LDB6Ii8+Cgk8cG9seWdvbiBzdHlsZT0iZmlsbDojRDlEN0NBOyIgcG9pbnRzPSIzNy41LDAuMTUxIDM3LjUsMTIgNDkuMzQ5LDEyICAiLz4KCTxwYXRoIHN0eWxlPSJmaWxsOiNFRUFGNEI7IiBkPSJNNDguMDM3LDU2SDcuOTYzQzcuMTU1LDU2LDYuNSw1NS4zNDUsNi41LDU0LjUzN1YzOWg0M3YxNS41MzdDNDkuNSw1NS4zNDUsNDguODQ1LDU2LDQ4LjAzNyw1NnoiLz4KCTxnPgoJCTxwYXRoIHN0eWxlPSJmaWxsOiNGRkZGRkY7IiBkPSJNMjYuMDIxLDQyLjcxOXY3Ljg0OGMwLDAuNDc0LTAuMDg3LDAuODczLTAuMjYsMS4xOTZjLTAuMTc0LDAuMzIzLTAuNDA2LDAuNTgzLTAuNjk3LDAuNzc5ICAgIGMtMC4yOTIsMC4xOTYtMC42MjcsMC4zMzMtMS4wMDUsMC40MXMtMC43NjksMC4xMTYtMS4xNjksMC4xMTZjLTAuMjAxLDAtMC40MzYtMC4wMjEtMC43MDQtMC4wNjJzLTAuNTQ3LTAuMTA0LTAuODM0LTAuMTkxICAgIHMtMC41NjMtMC4xODUtMC44MjctMC4yOTRjLTAuMjY1LTAuMTA5LTAuNDg4LTAuMjMyLTAuNjctMC4zNjlsMC42OTctMS4xMDdjMC4wOTEsMC4wNjMsMC4yMjEsMC4xMywwLjM5LDAuMTk4ICAgIHMwLjM1MywwLjEzMiwwLjU1NCwwLjE5MWMwLjIsMC4wNiwwLjQxLDAuMTExLDAuNjI5LDAuMTU3czAuNDI0LDAuMDY4LDAuNjE1LDAuMDY4YzAuNDgyLDAsMC44NjgtMC4wOTQsMS4xNTUtMC4yOCAgICBzMC40MzktMC41MDQsMC40NTgtMC45NXYtNy43MTFIMjYuMDIxeiIvPgoJCTxwYXRoIHN0eWxlPSJmaWxsOiNGRkZGRkY7IiBkPSJNMzQuMTg0LDUwLjIzOGMwLDAuMzY0LTAuMDc1LDAuNzE4LTAuMjI2LDEuMDZzLTAuMzYyLDAuNjQzLTAuNjM2LDAuOTAycy0wLjYxMSwwLjQ2Ny0xLjAxMiwwLjYyMiAgICBjLTAuNDAxLDAuMTU1LTAuODU3LDAuMjMyLTEuMzY3LDAuMjMyYy0wLjIxOSwwLTAuNDQ0LTAuMDEyLTAuNjc3LTAuMDM0cy0wLjQ2OC0wLjA2Mi0wLjcwNC0wLjExNiAgICBjLTAuMjM3LTAuMDU1LTAuNDYzLTAuMTMtMC42NzctMC4yMjZzLTAuMzk5LTAuMjEyLTAuNTU0LTAuMzQ5bDAuMjg3LTEuMTc2YzAuMTI3LDAuMDczLDAuMjg5LDAuMTQ0LDAuNDg1LDAuMjEyICAgIHMwLjM5OCwwLjEzMiwwLjYwOCwwLjE5MWMwLjIwOSwwLjA2LDAuNDE5LDAuMTA3LDAuNjI5LDAuMTQ0YzAuMjA5LDAuMDM2LDAuNDA1LDAuMDU1LDAuNTg4LDAuMDU1YzAuNTU2LDAsMC45ODItMC4xMywxLjI3OC0wLjM5ICAgIHMwLjQ0NC0wLjY0NSwwLjQ0NC0xLjE1NWMwLTAuMzEtMC4xMDUtMC41NzQtMC4zMTQtMC43OTNjLTAuMjEtMC4yMTktMC40NzItMC40MTctMC43ODYtMC41OTVzLTAuNjU0LTAuMzU1LTEuMDE5LTAuNTMzICAgIGMtMC4zNjUtMC4xNzgtMC43MDctMC4zODgtMS4wMjUtMC42MjljLTAuMzE5LTAuMjQxLTAuNTg0LTAuNTI2LTAuNzkzLTAuODU0Yy0wLjIxLTAuMzI4LTAuMzE0LTAuNzM4LTAuMzE0LTEuMjMgICAgYzAtMC40NDYsMC4wODItMC44NDMsMC4yNDYtMS4xODlzMC4zODUtMC42NDEsMC42NjMtMC44ODJzMC42MDItMC40MjYsMC45NzEtMC41NTRzMC43NTktMC4xOTEsMS4xNjktMC4xOTEgICAgYzAuNDE5LDAsMC44NDMsMC4wMzksMS4yNzEsMC4xMTZjMC40MjgsMC4wNzcsMC43NzQsMC4yMDMsMS4wMzksMC4zNzZjLTAuMDU1LDAuMTE4LTAuMTE5LDAuMjQ4LTAuMTkxLDAuMzkgICAgYy0wLjA3MywwLjE0Mi0wLjE0MiwwLjI3My0wLjIwNSwwLjM5NmMtMC4wNjQsMC4xMjMtMC4xMTksMC4yMjYtMC4xNjQsMC4zMDhjLTAuMDQ2LDAuMDgyLTAuMDczLDAuMTI4LTAuMDgyLDAuMTM3ICAgIGMtMC4wNTUtMC4wMjctMC4xMTYtMC4wNjMtMC4xODUtMC4xMDlzLTAuMTY3LTAuMDkxLTAuMjk0LTAuMTM3Yy0wLjEyOC0wLjA0Ni0wLjI5Ny0wLjA3Ny0wLjUwNi0wLjA5NiAgICBjLTAuMjEtMC4wMTktMC40NzktMC4wMTQtMC44MDcsMC4wMTRjLTAuMTgzLDAuMDE5LTAuMzU1LDAuMDctMC41MiwwLjE1N3MtMC4zMTEsMC4xOTMtMC40MzgsMC4zMjEgICAgYy0wLjEyOCwwLjEyOC0wLjIyOSwwLjI3MS0wLjMwMSwwLjQzMWMtMC4wNzMsMC4xNTktMC4xMDksMC4zMTMtMC4xMDksMC40NThjMCwwLjM2NCwwLjEwNCwwLjY1OCwwLjMxNCwwLjg4MiAgICBjMC4yMDksMC4yMjQsMC40NjksMC40MTksMC43NzksMC41ODhjMC4zMSwwLjE2OSwwLjY0NiwwLjMzMywxLjAxMiwwLjQ5MmMwLjM2NCwwLjE1OSwwLjcwNCwwLjM1NCwxLjAxOSwwLjU4MSAgICBzMC41NzYsMC41MTMsMC43ODYsMC44NTRDMzQuMDc4LDQ5LjI2MSwzNC4xODQsNDkuNywzNC4xODQsNTAuMjM4eiIvPgoJPC9nPgoJPGc+CgkJPHBhdGggc3R5bGU9ImZpbGw6I0VFQUY0QjsiIGQ9Ik0xOS41LDE5di00YzAtMC41NTEsMC40NDgtMSwxLTFjMC41NTMsMCwxLTAuNDQ4LDEtMXMtMC40NDctMS0xLTFjLTEuNjU0LDAtMywxLjM0Ni0zLDN2NCAgICBjMCwxLjEwMy0wLjg5NywyLTIsMmMtMC41NTMsMC0xLDAuNDQ4LTEsMXMwLjQ0NywxLDEsMWMxLjEwMywwLDIsMC44OTcsMiwydjRjMCwxLjY1NCwxLjM0NiwzLDMsM2MwLjU1MywwLDEtMC40NDgsMS0xICAgIHMtMC40NDctMS0xLTFjLTAuNTUyLDAtMS0wLjQ0OS0xLTF2LTRjMC0xLjItMC41NDItMi4yNjYtMS4zODItM0MxOC45NTgsMjEuMjY2LDE5LjUsMjAuMiwxOS41LDE5eiIvPgoJCTxjaXJjbGUgc3R5bGU9ImZpbGw6I0VFQUY0QjsiIGN4PSIyNy41IiBjeT0iMTguNSIgcj0iMS41Ii8+CgkJPHBhdGggc3R5bGU9ImZpbGw6I0VFQUY0QjsiIGQ9Ik0zOS41LDIxYy0xLjEwMywwLTItMC44OTctMi0ydi00YzAtMS42NTQtMS4zNDYtMy0zLTNjLTAuNTUzLDAtMSwwLjQ0OC0xLDFzMC40NDcsMSwxLDEgICAgYzAuNTUyLDAsMSwwLjQ0OSwxLDF2NGMwLDEuMiwwLjU0MiwyLjI2NiwxLjM4MiwzYy0wLjg0LDAuNzM0LTEuMzgyLDEuOC0xLjM4MiwzdjRjMCwwLjU1MS0wLjQ0OCwxLTEsMWMtMC41NTMsMC0xLDAuNDQ4LTEsMSAgICBzMC40NDcsMSwxLDFjMS42NTQsMCwzLTEuMzQ2LDMtM3YtNGMwLTEuMTAzLDAuODk3LTIsMi0yYzAuNTUzLDAsMS0wLjQ0OCwxLTFTNDAuMDUzLDIxLDM5LjUsMjF6Ii8+CgkJPHBhdGggc3R5bGU9ImZpbGw6I0VFQUY0QjsiIGQ9Ik0yNy41LDI0Yy0wLjU1MywwLTEsMC40NDgtMSwxdjNjMCwwLjU1MiwwLjQ0NywxLDEsMXMxLTAuNDQ4LDEtMXYtMyAgICBDMjguNSwyNC40NDgsMjguMDUzLDI0LDI3LjUsMjR6Ii8+Cgk8L2c+CjwvZz4KPGc+CjwvZz4KPGc+CjwvZz4KPGc+CjwvZz4KPGc+CjwvZz4KPGc+CjwvZz4KPGc+CjwvZz4KPGc+CjwvZz4KPGc+CjwvZz4KPGc+CjwvZz4KPGc+CjwvZz4KPGc+CjwvZz4KPGc+CjwvZz4KPGc+CjwvZz4KPGc+CjwvZz4KPGc+CjwvZz4KPC9zdmc+Cg==";
        private const string cssIcon = "PD94bWwgdmVyc2lvbj0iMS4wIiBlbmNvZGluZz0iaXNvLTg4NTktMSI/Pgo8IS0tIEdlbmVyYXRvcjogQWRvYmUgSWxsdXN0cmF0b3IgMTguMC4wLCBTVkcgRXhwb3J0IFBsdWctSW4gLiBTVkcgVmVyc2lvbjogNi4wMCBCdWlsZCAwKSAgLS0+CjwhRE9DVFlQRSBzdmcgUFVCTElDICItLy9XM0MvL0RURCBTVkcgMS4xLy9FTiIgImh0dHA6Ly93d3cudzMub3JnL0dyYXBoaWNzL1NWRy8xLjEvRFREL3N2ZzExLmR0ZCI+CjxzdmcgeG1sbnM9Imh0dHA6Ly93d3cudzMub3JnLzIwMDAvc3ZnIiB4bWxuczp4bGluaz0iaHR0cDovL3d3dy53My5vcmcvMTk5OS94bGluayIgdmVyc2lvbj0iMS4xIiBpZD0iQ2FwYV8xIiB4PSIwcHgiIHk9IjBweCIgdmlld0JveD0iMCAwIDU2IDU2IiBzdHlsZT0iZW5hYmxlLWJhY2tncm91bmQ6bmV3IDAgMCA1NiA1NjsiIHhtbDpzcGFjZT0icHJlc2VydmUiIHdpZHRoPSI1MTJweCIgaGVpZ2h0PSI1MTJweCI+CjxnPgoJPHBhdGggc3R5bGU9ImZpbGw6I0U5RTlFMDsiIGQ9Ik0zNi45ODUsMEg3Ljk2M0M3LjE1NSwwLDYuNSwwLjY1NSw2LjUsMS45MjZWNTVjMCwwLjM0NSwwLjY1NSwxLDEuNDYzLDFoNDAuMDc0ICAgYzAuODA4LDAsMS40NjMtMC42NTUsMS40NjMtMVYxMi45NzhjMC0wLjY5Ni0wLjA5My0wLjkyLTAuMjU3LTEuMDg1TDM3LjYwNywwLjI1N0MzNy40NDIsMC4wOTMsMzcuMjE4LDAsMzYuOTg1LDB6Ii8+Cgk8cG9seWdvbiBzdHlsZT0iZmlsbDojRDlEN0NBOyIgcG9pbnRzPSIzNy41LDAuMTUxIDM3LjUsMTIgNDkuMzQ5LDEyICAiLz4KCTxwYXRoIHN0eWxlPSJmaWxsOiMwMDk2RTY7IiBkPSJNNDguMDM3LDU2SDcuOTYzQzcuMTU1LDU2LDYuNSw1NS4zNDUsNi41LDU0LjUzN1YzOWg0M3YxNS41MzdDNDkuNSw1NS4zNDUsNDguODQ1LDU2LDQ4LjAzNyw1NnoiLz4KCTxnPgoJCTxwYXRoIHN0eWxlPSJmaWxsOiNGRkZGRkY7IiBkPSJNMjMuNTgsNTEuOTc1Yy0wLjM3NCwwLjM2NC0wLjc5OCwwLjYzOC0xLjI3MSwwLjgycy0wLjk4NCwwLjI3My0xLjUzMSwwLjI3MyAgICBjLTAuNjAyLDAtMS4xNTUtMC4xMDktMS42NjEtMC4zMjhzLTAuOTQ4LTAuNTQyLTEuMzI2LTAuOTcxcy0wLjY3NS0wLjk2Ni0wLjg4OS0xLjYxM2MtMC4yMTQtMC42NDctMC4zMjEtMS4zOTUtMC4zMjEtMi4yNDIgICAgczAuMTA3LTEuNTkzLDAuMzIxLTIuMjM1YzAuMjE0LTAuNjQzLDAuNTExLTEuMTc4LDAuODg5LTEuNjA2czAuODIyLTAuNzU0LDEuMzMzLTAuOTc4czEuMDYyLTAuMzM1LDEuNjU0LTAuMzM1ICAgIGMwLjU0NywwLDEuMDU4LDAuMDkxLDEuNTMxLDAuMjczczAuODk3LDAuNDU2LDEuMjcxLDAuODJsLTEuMTM1LDEuMDEyYy0wLjIyOC0wLjI2NS0wLjQ4LTAuNDU2LTAuNzU5LTAuNTc0ICAgIHMtMC41NjctMC4xNzgtMC44NjgtMC4xNzhjLTAuMzM3LDAtMC42NTgsMC4wNjMtMC45NjQsMC4xOTFzLTAuNTc5LDAuMzQ0LTAuODIsMC42NDlzLTAuNDMxLDAuNjk5LTAuNTY3LDEuMTgzICAgIHMtMC4yMSwxLjA3NS0wLjIxOSwxLjc3N2MwLjAwOSwwLjY4NCwwLjA4LDEuMjY3LDAuMjEyLDEuNzVzMC4zMTQsMC44NzcsMC41NDcsMS4xODNzMC40OTcsMC41MjgsMC43OTMsMC42NyAgICBzMC42MDgsMC4yMTIsMC45MzcsMC4yMTJzMC42MzYtMC4wNiwwLjkyMy0wLjE3OHMwLjU0OS0wLjMxLDAuNzg2LTAuNTc0TDIzLjU4LDUxLjk3NXoiLz4KCQk8cGF0aCBzdHlsZT0iZmlsbDojRkZGRkZGOyIgZD0iTTMxLjYzMyw1MC4yMzhjMCwwLjM2NC0wLjA3NSwwLjcxOC0wLjIyNiwxLjA2cy0wLjM2MiwwLjY0My0wLjYzNiwwLjkwMnMtMC42MSwwLjQ2Ny0xLjAxMiwwLjYyMiAgICBzLTAuODU2LDAuMjMyLTEuMzY3LDAuMjMyYy0wLjIxOSwwLTAuNDQ0LTAuMDEyLTAuNjc3LTAuMDM0cy0wLjQ2Ny0wLjA2Mi0wLjcwNC0wLjExNnMtMC40NjMtMC4xMy0wLjY3Ny0wLjIyNiAgICBzLTAuMzk4LTAuMjEyLTAuNTU0LTAuMzQ5bDAuMjg3LTEuMTc2YzAuMTI4LDAuMDczLDAuMjg5LDAuMTQ0LDAuNDg1LDAuMjEyczAuMzk4LDAuMTMyLDAuNjA4LDAuMTkxczAuNDE5LDAuMTA3LDAuNjI5LDAuMTQ0ICAgIHMwLjQwNSwwLjA1NSwwLjU4OCwwLjA1NWMwLjU1NiwwLDAuOTgyLTAuMTMsMS4yNzgtMC4zOXMwLjQ0NC0wLjY0NSwwLjQ0NC0xLjE1NWMwLTAuMzEtMC4xMDQtMC41NzQtMC4zMTQtMC43OTMgICAgcy0wLjQ3Mi0wLjQxNy0wLjc4Ni0wLjU5NXMtMC42NTQtMC4zNTUtMS4wMTktMC41MzNzLTAuNzA2LTAuMzg4LTEuMDI1LTAuNjI5cy0wLjU4My0wLjUyNi0wLjc5My0wLjg1NHMtMC4zMTQtMC43MzgtMC4zMTQtMS4yMyAgICBjMC0wLjQ0NiwwLjA4Mi0wLjg0MywwLjI0Ni0xLjE4OXMwLjM4NS0wLjY0MSwwLjY2My0wLjg4MnMwLjYwMi0wLjQyNiwwLjk3MS0wLjU1NHMwLjc1OS0wLjE5MSwxLjE2OS0wLjE5MSAgICBjMC40MTksMCwwLjg0MywwLjAzOSwxLjI3MSwwLjExNnMwLjc3NCwwLjIwMywxLjAzOSwwLjM3NmMtMC4wNTUsMC4xMTgtMC4xMTgsMC4yNDgtMC4xOTEsMC4zOXMtMC4xNDIsMC4yNzMtMC4yMDUsMC4zOTYgICAgYy0wLjA2MywwLjEyMy0wLjExOCwwLjIyNi0wLjE2NCwwLjMwOHMtMC4wNzMsMC4xMjgtMC4wODIsMC4xMzdjLTAuMDU1LTAuMDI3LTAuMTE2LTAuMDYzLTAuMTg1LTAuMTA5cy0wLjE2Ni0wLjA5MS0wLjI5NC0wLjEzNyAgICBzLTAuMjk2LTAuMDc3LTAuNTA2LTAuMDk2cy0wLjQ3OS0wLjAxNC0wLjgwNywwLjAxNGMtMC4xODMsMC4wMTktMC4zNTUsMC4wNy0wLjUyLDAuMTU3cy0wLjMxLDAuMTkzLTAuNDM4LDAuMzIxICAgIHMtMC4yMjgsMC4yNzEtMC4zMDEsMC40MzFzLTAuMTA5LDAuMzEzLTAuMTA5LDAuNDU4YzAsMC4zNjQsMC4xMDQsMC42NTgsMC4zMTQsMC44ODJzMC40NywwLjQxOSwwLjc3OSwwLjU4OCAgICBzMC42NDcsMC4zMzMsMS4wMTIsMC40OTJzMC43MDQsMC4zNTQsMS4wMTksMC41ODFzMC41NzYsMC41MTMsMC43ODYsMC44NTRTMzEuNjMzLDQ5LjcsMzEuNjMzLDUwLjIzOHoiLz4KCQk8cGF0aCBzdHlsZT0iZmlsbDojRkZGRkZGOyIgZD0iTTM5LjA0Myw1MC4yMzhjMCwwLjM2NC0wLjA3NSwwLjcxOC0wLjIyNiwxLjA2cy0wLjM2MiwwLjY0My0wLjYzNiwwLjkwMnMtMC42MSwwLjQ2Ny0xLjAxMiwwLjYyMiAgICBzLTAuODU2LDAuMjMyLTEuMzY3LDAuMjMyYy0wLjIxOSwwLTAuNDQ0LTAuMDEyLTAuNjc3LTAuMDM0cy0wLjQ2Ny0wLjA2Mi0wLjcwNC0wLjExNnMtMC40NjMtMC4xMy0wLjY3Ny0wLjIyNiAgICBzLTAuMzk4LTAuMjEyLTAuNTU0LTAuMzQ5bDAuMjg3LTEuMTc2YzAuMTI4LDAuMDczLDAuMjg5LDAuMTQ0LDAuNDg1LDAuMjEyczAuMzk4LDAuMTMyLDAuNjA4LDAuMTkxczAuNDE5LDAuMTA3LDAuNjI5LDAuMTQ0ICAgIHMwLjQwNSwwLjA1NSwwLjU4OCwwLjA1NWMwLjU1NiwwLDAuOTgyLTAuMTMsMS4yNzgtMC4zOXMwLjQ0NC0wLjY0NSwwLjQ0NC0xLjE1NWMwLTAuMzEtMC4xMDQtMC41NzQtMC4zMTQtMC43OTMgICAgcy0wLjQ3Mi0wLjQxNy0wLjc4Ni0wLjU5NXMtMC42NTQtMC4zNTUtMS4wMTktMC41MzNzLTAuNzA2LTAuMzg4LTEuMDI1LTAuNjI5cy0wLjU4My0wLjUyNi0wLjc5My0wLjg1NHMtMC4zMTQtMC43MzgtMC4zMTQtMS4yMyAgICBjMC0wLjQ0NiwwLjA4Mi0wLjg0MywwLjI0Ni0xLjE4OXMwLjM4NS0wLjY0MSwwLjY2My0wLjg4MnMwLjYwMi0wLjQyNiwwLjk3MS0wLjU1NHMwLjc1OS0wLjE5MSwxLjE2OS0wLjE5MSAgICBjMC40MTksMCwwLjg0MywwLjAzOSwxLjI3MSwwLjExNnMwLjc3NCwwLjIwMywxLjAzOSwwLjM3NmMtMC4wNTUsMC4xMTgtMC4xMTgsMC4yNDgtMC4xOTEsMC4zOXMtMC4xNDIsMC4yNzMtMC4yMDUsMC4zOTYgICAgcy0wLjExOCwwLjIyNi0wLjE2NCwwLjMwOHMtMC4wNzMsMC4xMjgtMC4wODIsMC4xMzdjLTAuMDU1LTAuMDI3LTAuMTE2LTAuMDYzLTAuMTg1LTAuMTA5cy0wLjE2Ni0wLjA5MS0wLjI5NC0wLjEzNyAgICBzLTAuMjk2LTAuMDc3LTAuNTA2LTAuMDk2cy0wLjQ3OS0wLjAxNC0wLjgwNywwLjAxNGMtMC4xODMsMC4wMTktMC4zNTUsMC4wNy0wLjUyLDAuMTU3cy0wLjMxLDAuMTkzLTAuNDM4LDAuMzIxICAgIHMtMC4yMjgsMC4yNzEtMC4zMDEsMC40MzFzLTAuMTA5LDAuMzEzLTAuMTA5LDAuNDU4YzAsMC4zNjQsMC4xMDQsMC42NTgsMC4zMTQsMC44ODJzMC40NywwLjQxOSwwLjc3OSwwLjU4OCAgICBzMC42NDcsMC4zMzMsMS4wMTIsMC40OTJzMC43MDQsMC4zNTQsMS4wMTksMC41ODFzMC41NzYsMC41MTMsMC43ODYsMC44NTRTMzkuMDQzLDQ5LjcsMzkuMDQzLDUwLjIzOHoiLz4KCTwvZz4KCTxnPgoJCTxwYXRoIHN0eWxlPSJmaWxsOiMwMDk2RTY7IiBkPSJNMTkuNSwxOXYtNGMwLTAuNTUxLDAuNDQ4LTEsMS0xYzAuNTUzLDAsMS0wLjQ0OCwxLTFzLTAuNDQ3LTEtMS0xYy0xLjY1NCwwLTMsMS4zNDYtMywzdjQgICAgYzAsMS4xMDMtMC44OTcsMi0yLDJjLTAuNTUzLDAtMSwwLjQ0OC0xLDFzMC40NDcsMSwxLDFjMS4xMDMsMCwyLDAuODk3LDIsMnY0YzAsMS42NTQsMS4zNDYsMywzLDNjMC41NTMsMCwxLTAuNDQ4LDEtMSAgICBzLTAuNDQ3LTEtMS0xYy0wLjU1MiwwLTEtMC40NDktMS0xdi00YzAtMS4yLTAuNTQyLTIuMjY2LTEuMzgyLTNDMTguOTU4LDIxLjI2NiwxOS41LDIwLjIsMTkuNSwxOXoiLz4KCQk8cGF0aCBzdHlsZT0iZmlsbDojMDA5NkU2OyIgZD0iTTM5LjUsMjFjLTEuMTAzLDAtMi0wLjg5Ny0yLTJ2LTRjMC0xLjY1NC0xLjM0Ni0zLTMtM2MtMC41NTMsMC0xLDAuNDQ4LTEsMXMwLjQ0NywxLDEsMSAgICBjMC41NTIsMCwxLDAuNDQ5LDEsMXY0YzAsMS4yLDAuNTQyLDIuMjY2LDEuMzgyLDNjLTAuODQsMC43MzQtMS4zODIsMS44LTEuMzgyLDN2NGMwLDAuNTUxLTAuNDQ4LDEtMSwxYy0wLjU1MywwLTEsMC40NDgtMSwxICAgIHMwLjQ0NywxLDEsMWMxLjY1NCwwLDMtMS4zNDYsMy0zdi00YzAtMS4xMDMsMC44OTctMiwyLTJjMC41NTMsMCwxLTAuNDQ4LDEtMVM0MC4wNTMsMjEsMzkuNSwyMXoiLz4KCTwvZz4KPC9nPgo8Zz4KPC9nPgo8Zz4KPC9nPgo8Zz4KPC9nPgo8Zz4KPC9nPgo8Zz4KPC9nPgo8Zz4KPC9nPgo8Zz4KPC9nPgo8Zz4KPC9nPgo8Zz4KPC9nPgo8Zz4KPC9nPgo8Zz4KPC9nPgo8Zz4KPC9nPgo8Zz4KPC9nPgo8Zz4KPC9nPgo8Zz4KPC9nPgo8L3N2Zz4K";
        private const string htmlIcon = "PD94bWwgdmVyc2lvbj0iMS4wIiBlbmNvZGluZz0iaXNvLTg4NTktMSI/Pgo8IS0tIEdlbmVyYXRvcjogQWRvYmUgSWxsdXN0cmF0b3IgMTkuMC4wLCBTVkcgRXhwb3J0IFBsdWctSW4gLiBTVkcgVmVyc2lvbjogNi4wMCBCdWlsZCAwKSAgLS0+CjxzdmcgeG1sbnM9Imh0dHA6Ly93d3cudzMub3JnLzIwMDAvc3ZnIiB4bWxuczp4bGluaz0iaHR0cDovL3d3dy53My5vcmcvMTk5OS94bGluayIgdmVyc2lvbj0iMS4xIiBpZD0iTGF5ZXJfMSIgeD0iMHB4IiB5PSIwcHgiIHZpZXdCb3g9IjAgMCA1MTIgNTEyIiBzdHlsZT0iZW5hYmxlLWJhY2tncm91bmQ6bmV3IDAgMCA1MTIgNTEyOyIgeG1sOnNwYWNlPSJwcmVzZXJ2ZSIgd2lkdGg9IjUxMnB4IiBoZWlnaHQ9IjUxMnB4Ij4KPHBhdGggc3R5bGU9ImZpbGw6I0U2RUFFQTsiIGQ9Ik01MDMuOTgzLDQuMjc2SDguMDE3QzMuNTg5LDQuMjc2LDAsNy44NjUsMCwxMi4yOTJ2NDg3LjQxNWMwLDQuNDI3LDMuNTg5LDguMDE3LDguMDE3LDguMDE3aDQ5NS45NjcgIGM0LjQyNywwLDguMDE3LTMuNTg5LDguMDE3LTguMDE3VjEyLjI5MkM1MTIsNy44NjUsNTA4LjQxMSw0LjI3Niw1MDMuOTgzLDQuMjc2eiIvPgo8cGF0aCBzdHlsZT0iZmlsbDojOUJBQUFCOyIgZD0iTTUwMy45ODMsNC4yNzZIOC4wMTdDMy41ODksNC4yNzYsMCw3Ljg2NSwwLDEyLjI5MnYxMTAuNjNoNTEyVjEyLjI5MiAgQzUxMiw3Ljg2NSw1MDguNDExLDQuMjc2LDUwMy45ODMsNC4yNzZ6Ii8+CjxwYXRoIHN0eWxlPSJmaWxsOiNDREQ0RDU7IiBkPSJNOC4wMTcsNC4yNzZDMy41ODksNC4yNzYsMCw3Ljg2NSwwLDEyLjI5MnY0ODcuNDE1YzAsNC40MjcsMy41ODksOC4wMTcsOC4wMTcsOC4wMTdIMjU2VjQuMjc2SDguMDE3ICB6Ii8+CjxwYXRoIHN0eWxlPSJmaWxsOiM2ODdGODI7IiBkPSJNOC4wMTcsNC4yNzZDMy41ODksNC4yNzYsMCw3Ljg2NSwwLDEyLjI5MnYxMTAuNjNoMjU2VjQuMjc2SDguMDE3eiIvPgo8cGF0aCBzdHlsZT0iZmlsbDojNEFDRkQ5OyIgZD0iTTI2NC41NTEsMjkuOTI5Yy0xOC41NjYsMC0zMy42NywxNS4xMDUtMzMuNjcsMzMuNjdzMTUuMTA1LDMzLjY3LDMzLjY3LDMzLjY3ICBzMzMuNjctMTUuMTA1LDMzLjY3LTMzLjY3UzI4My4xMTcsMjkuOTI5LDI2NC41NTEsMjkuOTI5eiIvPgo8cGF0aCBzdHlsZT0iZmlsbDojRkY4QzI5OyIgZD0iTTE3MC40ODksMjkuOTI5Yy0xOC41NjYsMC0zMy42NywxNS4xMDUtMzMuNjcsMzMuNjdzMTUuMTA1LDMzLjY3LDMzLjY3LDMzLjY3ICBzMzMuNjctMTUuMTA1LDMzLjY3LTMzLjY3UzE4OS4wNTQsMjkuOTI5LDE3MC40ODksMjkuOTI5eiIvPgo8cGF0aCBzdHlsZT0iZmlsbDojRjAzNTNEOyIgZD0iTTc2LjQyNiwyOS45MjljLTE4LjU2NiwwLTMzLjY3LDE1LjEwNS0zMy42NywzMy42N3MxNS4xMDUsMzMuNjcsMzMuNjcsMzMuNjcgIHMzMy42Ny0xNS4xMDUsMzMuNjctMzMuNjdTOTQuOTkyLDI5LjkyOSw3Ni40MjYsMjkuOTI5eiIvPgo8Zz4KCTxwYXRoIHN0eWxlPSJmaWxsOiNGRkZGRkY7IiBkPSJNNDI3LjAyMyw1NC41MTRIMzMyLjk2Yy00LjQyOCwwLTguMDE3LTMuNTg5LTguMDE3LTguMDE3czMuNTg4LTguMDE3LDguMDE3LTguMDE3aDk0LjA2MyAgIGM0LjQyOCwwLDguMDE3LDMuNTg5LDguMDE3LDguMDE3UzQzMS40NTEsNTQuNTE0LDQyNy4wMjMsNTQuNTE0eiIvPgoJPHBhdGggc3R5bGU9ImZpbGw6I0ZGRkZGRjsiIGQ9Ik00NjkuNzc5LDU0LjUxNGgtMTcuMTAyYy00LjQyOCwwLTguMDE3LTMuNTg5LTguMDE3LTguMDE3czMuNTg4LTguMDE3LDguMDE3LTguMDE3aDE3LjEwMiAgIGM0LjQyOCwwLDguMDE3LDMuNTg5LDguMDE3LDguMDE3UzQ3NC4yMDcsNTQuNTE0LDQ2OS43NzksNTQuNTE0eiIvPgo8L2c+CjxnPgoJPHBhdGggc3R5bGU9ImZpbGw6I0ZENkEzMzsiIGQ9Ik00NjQuOTQyLDI5MC45MTRsLTgxLjIzNi03Ni45NTljLTQuNjY0LTQuNDE5LTEwLjc3MS02Ljc2Ny0xNy4xOS02LjU4MyAgIGMtNi40MjIsMC4xNzQtMTIuMzkyLDIuODM4LTE2LjgwOSw3LjUwMWMtNC40MTksNC42NjQtNi43NTcsMTAuNzY4LTYuNTgzLDE3LjE5MWMwLjE3NCw2LjQyMSwyLjgzNywxMi4zOTEsNy41MDEsMTYuODA5ICAgbDYyLjI3NCw1OC45OTVsLTYxLjc2OSw1NS4yNjdjLTQuNzg4LDQuMjgzLTcuNjIsMTAuMTc1LTcuOTc3LDE2LjU4OWMtMC4zNTYsNi40MTQsMS44MDYsMTIuNTgzLDYuMDkxLDE3LjM3MSAgIGM0LjU1Nyw1LjA5MiwxMS4wOTEsOC4wMTIsMTcuOTI3LDguMDE0YzAuMDAxLDAsMC4wMDIsMCwwLjAwMywwYzUuOTE4LDAsMTEuNjExLTIuMTc2LDE2LjAyOS02LjEyN2w4MS4yMzYtNzIuNjg1ICAgYzQuOTk2LTQuNDcsNy45MTUtMTAuODc4LDguMDEtMTcuNTgxQzQ3Mi41NDMsMzAyLjAxMiw0NjkuODA3LDI5NS41MjQsNDY0Ljk0MiwyOTAuOTE0eiIvPgoJPHBhdGggc3R5bGU9ImZpbGw6I0ZENkEzMzsiIGQ9Ik0zMDYuNDgsMjE5LjEyMWMtMTEuNDk1LTYuNjE0LTI2LjIyNi0yLjY0Ny0zMi44NDIsOC44NDNsLTgxLjIzNiwxNDEuMDk0ICAgYy02LjYxOCwxMS40OTItMi42NSwyNi4yMjUsOC44NDMsMzIuODQzYzMuNjQ4LDIuMDk5LDcuNzkyLDMuMjEsMTEuOTgzLDMuMjFjOC41ODUsMCwxNi41NzctNC42MTksMjAuODU3LTEyLjA1M2w4MS4yMzYtMTQxLjA5MyAgIGMzLjIwNS01LjU2Nyw0LjA1MS0xMi4wNSwyLjM4Mi0xOC4yNTJDMzE2LjAzMywyMjcuNTA5LDMxMi4wNDcsMjIyLjMyNywzMDYuNDgsMjE5LjEyMXoiLz4KPC9nPgo8Zz4KCTxwYXRoIHN0eWxlPSJmaWxsOiNGMDM1M0Q7IiBkPSJNMTkyLjQwMiwzNjkuMDU4Yy02LjYxOCwxMS40OTItMi42NSwyNi4yMjUsOC44NDMsMzIuODQzYzMuNjQ4LDIuMDk5LDcuNzkyLDMuMjEsMTEuOTgzLDMuMjEgICBjOC41ODUsMCwxNi41NzctNC42MTksMjAuODU3LTEyLjA1M2wyMS45MTMtMzguMDZ2LTk2LjRMMTkyLjQwMiwzNjkuMDU4eiIvPgoJPHBhdGggc3R5bGU9ImZpbGw6I0YwMzUzRDsiIGQ9Ik0xNjguODc3LDIzMi4wNjNjMC4xNzQtNi40MjItMi4xNjMtMTIuNTI3LTYuNTgyLTE3LjE5MWMtNC40MTktNC42NjQtMTAuMzg5LTcuMzI3LTE2LjgxMS03LjUwMSAgIGMtNi40MDMtMC4xNzUtMTIuNTI2LDIuMTY1LTE3LjE5LDYuNTgybC04MS4yMzYsNzYuOTZjLTQuODY2LDQuNjEtNy42MDIsMTEuMDk4LTcuNTA3LDE3LjgwMSAgIGMwLjA5NSw2LjcwMywzLjAxNSwxMy4xMTIsOC4wMTEsMTcuNTgxbDgxLjIzNyw3Mi42ODZjNC40MTcsMy45NTEsMTAuMTA5LDYuMTI2LDE2LjAyOSw2LjEyNmMwLjAwMSwwLDAuMDAyLDAsMC4wMDMsMCAgIGM2LjgzNiwwLDEzLjM3MS0yLjkyMSwxNy45MjctOC4wMTNjOC44NDItOS44ODMsNy45OTYtMjUuMTE3LTEuODg3LTMzLjk2bC02MS43NjktNTUuMjY3bDYyLjI3NC01OC45OTUgICBDMTY2LjAzOSwyNDQuNDU0LDE2OC43MDMsMjM4LjQ4NCwxNjguODc3LDIzMi4wNjN6Ii8+CjwvZz4KPGc+CjwvZz4KPGc+CjwvZz4KPGc+CjwvZz4KPGc+CjwvZz4KPGc+CjwvZz4KPGc+CjwvZz4KPGc+CjwvZz4KPGc+CjwvZz4KPGc+CjwvZz4KPGc+CjwvZz4KPGc+CjwvZz4KPGc+CjwvZz4KPGc+CjwvZz4KPGc+CjwvZz4KPGc+CjwvZz4KPC9zdmc+Cg==";

        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult GetImage(Guid id)
        {
            var image = Repository.Get<FileItem>().Where(x => x.Id == id).Execute().First();
            return new FileStreamResult(new System.IO.MemoryStream(image.File), "image/" + image.FileType);
        }

        [HttpPost]
        public async Task<ExternalActionResult> GetFolders()
        {
            var folders = await Repository.Get<Folder>().Where(x => !x.Parent_Id.HasValue).LoadChildren().IgnoreChildren(x => x.Files).ExecuteAsync();
            return await folders.ToJsonResultAsync();
        }

        [HttpPost]
        public ExternalActionResult GetFoldersAutoFill(string value)
        {
            var folders = Repository.Get<Folder>().Where(x => !x.Parent_Id.HasValue && x.Children.Any(a => a.Name.Contains(value))).LoadChildren().IgnoreChildren(x => x.Files).Execute().FirstOrDefault()?.Children;
            return folders.ToJsonResult();
        }

        [HttpPost]
        public void SaveFolder(Folder folder)
        {
            if (folder != null)
                Repository.Save(folder);
            Repository.SaveChanges();
        }

        [HttpPost]
        public void DeleteFolder(Guid folderId)
        {
            Repository.Get<Folder>().Where(x => x.Id == folderId).LoadChildren().IgnoreChildren(x => x.Files.Select(a => a.Folder)).Execute().ForEach(x => Repository.Delete(x));
            Repository.SaveChanges();
        }

        [HttpPost]
        public async Task<ExternalActionResult> Get(int pageNr, Guid folderId)
        {
            var quary = Repository.Get<FileItem>()
               .Where(x => x.Folder_Id == folderId).Skip((pageNr - 1) * SearchResultValue)
               .Take(SearchResultValue)
               .OrderBy(x => x.FileName)
               .LoadChildren(x => x.Folder).IgnoreChildren(x => x.Folder.Files);

            var files = await quary.ExecuteAsync();
            return await files.ToJsonResultAsync();
        }

        public ExternalActionResult GetTheme(Guid folderId)
        {
            var container = new { Css = new List<string>(), Js = new List<String>() };
            void GetItems(Folder folder)
            {
                if (folder == null)
                    return;
                if (folder.FolderType == EnumHelper.FolderTypes.CSS)
                    container.Css.AddRange(folder.Files.Select(x => Url.Action("LoadFiles", "FileUploader", new { }, Request.Url.Scheme) + "?fileId=" + x.Id));

                if (folder.FolderType == EnumHelper.FolderTypes.JAVASCRIPT)
                    container.Js.AddRange(folder.Files.Select(x => Url.Action("LoadFiles", "FileUploader", new { }, Request.Url.Scheme) + "?fileId=" + x.Id));
                if (folder.Children.Any())
                    folder.Children.ForEach(x => GetItems(x));
            }

            GetItems(Repository.Get<Folder>().Where(x => x.Id == folderId).LoadChildren().IgnoreChildren(x => x.Files.Select(a => a.Slider), x => x.Files.Select(a => a.Folder)).ExecuteFirstOrDefault());
            return container.ToJsonResult();
        }

        [HttpPost]
        public void Delete(List<Guid> items)
        {
            items.ForEach(a => Repository.Get<FileItem>().Where(x => x.Id == a).Remove());
            Repository.SaveChanges();
        }

        [HttpPost]
        public void SaveFileItem(FileItem file)
        {
            var item = file.Id.HasValue ? Repository.Get<FileItem>().Where(x => x.Id == file.Id).Execute().First() : file;
            item.FileName = file.FileName;
            item.Folder_Id = file.Folder_Id;
            if ((file.FileType == EnumHelper.AllowedFiles.JAVASCRIPT || file.FileType == EnumHelper.AllowedFiles.CSS || file.FileType == EnumHelper.AllowedFiles.HtmlEmbedded))
            {
                if (file.FileType == EnumHelper.AllowedFiles.CSS)
                    item.ThumpFile = Convert.FromBase64String(cssIcon);
                else if (item.FileType == EnumHelper.AllowedFiles.JAVASCRIPT)
                    item.ThumpFile = Convert.FromBase64String(jsIcon);
                else item.ThumpFile = Convert.FromBase64String(htmlIcon);
                if (file.Text != null)
                {
                    item.Text = HttpUtility.UrlDecode(file.Text);
                    item.File = Encoding.UTF8.GetBytes(item.Text);
                }
            }
            if (item.File == null)
                item.File = new byte[0];
            Repository.Save(item);
            Repository.SaveChanges();
        }

        [HttpPost]
        public void Save()
        {
            var fileItems = Repository.Get<FileItem>();
            string FileName = "";
            var folderId = Request.Form["folderId"];
            HttpFileCollectionBase files = Request.Files;
            for (int i = 0; i < files.Count; i++)
            {
                HttpPostedFileBase file = files[i];
                string fname;

                // Checking for Internet Explorer    
                if (Request.Browser.Browser.ToUpper() == "IE" || Request.Browser.Browser.ToUpper() == "INTERNETEXPLORER")
                {
                    string[] testfiles = file.FileName.Split(new char[] { '\\' });
                    fname = testfiles[testfiles.Length - 1];
                }
                else
                {
                    fname = file.FileName;
                    FileName = file.FileName;
                }


                using (var mem = new MemoryStream())
                {

                    file.InputStream.CopyTo(mem);
                    if (fname.EndsWith(EnumHelper.AllowedFiles.PNG.ToString(), StringComparison.CurrentCultureIgnoreCase)
                        || fname.EndsWith(EnumHelper.AllowedFiles.GIF.ToString(), StringComparison.CurrentCultureIgnoreCase)
                        || fname.EndsWith(EnumHelper.AllowedFiles.JPEG.ToString(), StringComparison.CurrentCultureIgnoreCase)
                        || fname.EndsWith(EnumHelper.AllowedFiles.JPG.ToString(), StringComparison.CurrentCultureIgnoreCase))
                    {
                        System.Drawing.Image fullsizeImage = System.Drawing.Image.FromStream(mem);
                        System.Drawing.Image newImage = fullsizeImage.GetThumbnailImage(32, 32, null, IntPtr.Zero);
                        using (var imgMem = new MemoryStream())
                        {
                            newImage.Save(imgMem, System.Drawing.Imaging.ImageFormat.Png);
                            fileItems.Add(new FileItem()
                            {
                                File = mem.ToArray(),
                                Length = file.ContentLength,
                                FileType = file.FileName.Split('.').ToList().Last().ToUpper().ConvertValue<EnumHelper.AllowedFiles>(),
                                FileName = fname,
                                Folder_Id = folderId.ConvertValue<Guid>(),
                                ThumpFile = imgMem.ToArray(),
                                Width = fullsizeImage.Width,
                                Height = fullsizeImage.Height
                            });
                        }
                    }

                }
            }
            fileItems.Save();
            fileItems.SaveChanges();
        }

    }
}