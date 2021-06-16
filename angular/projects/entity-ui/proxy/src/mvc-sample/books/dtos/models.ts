import type { EntityDto } from '@abp/ng.core';

export interface BookDetailDto extends EntityDto {
  outline?: string;
}

export interface BookDto extends EntityDto<string> {
  name: string;
  isbn: string;
  length: number;
  detail: BookDetailDto;
  tags: BookTagDto[];
}

export interface BookTagDto extends EntityDto {
  tag: string;
}

export interface CreateUpdateBookDetailDto {
  outline?: string;
}

export interface CreateUpdateBookDto {
  name?: string;
  isbn?: string;
  length: number;
  detail: CreateUpdateBookDetailDto;
  tags: CreateUpdateBookTagDto[];
}

export interface CreateUpdateBookTagDto {
  tag?: string;
}
