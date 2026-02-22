/* eslint-disable */
/* tslint:disable */
// @ts-nocheck
/*
 * ---------------------------------------------------------------
 * ## THIS FILE WAS GENERATED VIA SWAGGER-TYPESCRIPT-API        ##
 * ##                                                           ##
 * ## AUTHOR: acacode                                           ##
 * ## SOURCE: https://github.com/acacode/swagger-typescript-api ##
 * ---------------------------------------------------------------
 */

import {
  VideoGameAddRequest,
  VideoGameResponse,
  VideoGameUpdateRequest,
} from "./data-contracts";
import { ContentType, HttpClient, RequestParams } from "./http-client";

export class Api<
  SecurityDataType = unknown,
> extends HttpClient<SecurityDataType> {
  /**
   * No description
   *
   * @tags VideoGame
   * @name VideoGameList
   * @request GET:/api/VideoGame
   */
  videoGameList = (params: RequestParams = {}) =>
    this.request<VideoGameResponse[], any>({
      path: `/api/VideoGame`,
      method: "GET",
      format: "json",
      ...params,
    });
  /**
   * No description
   *
   * @tags VideoGame
   * @name VideoGameCreate
   * @request POST:/api/VideoGame
   */
  videoGameCreate = (data: VideoGameAddRequest, params: RequestParams = {}) =>
    this.request<string, any>({
      path: `/api/VideoGame`,
      method: "POST",
      body: data,
      type: ContentType.Json,
      format: "json",
      ...params,
    });
  /**
   * No description
   *
   * @tags VideoGame
   * @name VideoGameDetail
   * @request GET:/api/VideoGame/{id}
   */
  videoGameDetail = (id: string, params: RequestParams = {}) =>
    this.request<VideoGameResponse, any>({
      path: `/api/VideoGame/${id}`,
      method: "GET",
      format: "json",
      ...params,
    });
  /**
   * No description
   *
   * @tags VideoGame
   * @name VideoGameUpdate
   * @request PUT:/api/VideoGame/{id}
   */
  videoGameUpdate = (
    id: string,
    data: VideoGameUpdateRequest,
    params: RequestParams = {},
  ) =>
    this.request<void, any>({
      path: `/api/VideoGame/${id}`,
      method: "PUT",
      body: data,
      type: ContentType.Json,
      ...params,
    });
  /**
   * No description
   *
   * @tags VideoGame
   * @name VideoGameDelete
   * @request DELETE:/api/VideoGame/{id}
   */
  videoGameDelete = (id: string, params: RequestParams = {}) =>
    this.request<void, any>({
      path: `/api/VideoGame/${id}`,
      method: "DELETE",
      ...params,
    });
}
