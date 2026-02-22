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

export interface VideoGameAddRequest {
  title?: string | null;
  description?: string | null;
}

export interface VideoGameResponse {
  /** @format uuid */
  id?: string;
  title?: string | null;
  description?: string | null;
  /** @format byte */
  rowVersion?: string | null;
}

export interface VideoGameUpdateRequest {
  title?: string | null;
  description?: string | null;
  /** @format byte */
  rowVersion?: string | null;
}
