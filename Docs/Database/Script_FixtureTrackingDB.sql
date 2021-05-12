--
-- PostgreSQL database dump
--

-- Dumped from database version 13.1
-- Dumped by pg_dump version 13.1

-- Started on 2021-01-21 22:09:15

SET statement_timeout = 0;
SET lock_timeout = 0;
SET idle_in_transaction_session_timeout = 0;
SET client_encoding = 'UTF8';
SET standard_conforming_strings = on;
SELECT pg_catalog.set_config('search_path', '', false);
SET check_function_bodies = false;
SET xmloption = content;
SET client_min_messages = warning;
SET row_security = off;

--
-- TOC entry 2 (class 3079 OID 16813)
-- Name: uuid-ossp; Type: EXTENSION; Schema: -; Owner: -
--

CREATE EXTENSION IF NOT EXISTS "uuid-ossp" WITH SCHEMA public;


--
-- TOC entry 3107 (class 0 OID 0)
-- Dependencies: 2
-- Name: EXTENSION "uuid-ossp"; Type: COMMENT; Schema: -; Owner: 
--

COMMENT ON EXTENSION "uuid-ossp" IS 'generate universally unique identifiers (UUIDs)';


SET default_table_access_method = heap;

--
-- TOC entry 203 (class 1259 OID 16553)
-- Name: categories; Type: TABLE; Schema: public; Owner: -
--

CREATE TABLE public.categories (
    id smallint NOT NULL,
    name character varying(50) NOT NULL,
    description text,
    is_enable boolean DEFAULT true,
    created_at timestamp with time zone DEFAULT CURRENT_TIMESTAMP,
    updated_at timestamp with time zone DEFAULT CURRENT_TIMESTAMP
);


--
-- TOC entry 202 (class 1259 OID 16551)
-- Name: categories_id_seq; Type: SEQUENCE; Schema: public; Owner: -
--

CREATE SEQUENCE public.categories_id_seq
    AS smallint
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


--
-- TOC entry 3108 (class 0 OID 0)
-- Dependencies: 202
-- Name: categories_id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: -
--

ALTER SEQUENCE public.categories_id_seq OWNED BY public.categories.id;


--
-- TOC entry 207 (class 1259 OID 16629)
-- Name: debits; Type: TABLE; Schema: public; Owner: -
--

CREATE TABLE public.debits (
    id uuid DEFAULT uuid_generate_v4() NOT NULL,
    description text,
    date_debit timestamp with time zone NOT NULL,
    date_return timestamp with time zone,
    is_return boolean DEFAULT false,
    created_at timestamp with time zone DEFAULT CURRENT_TIMESTAMP,
    updated_at timestamp with time zone DEFAULT CURRENT_TIMESTAMP,
    fixture_id uuid NOT NULL,
    user_id uuid NOT NULL
);


--
-- TOC entry 214 (class 1259 OID 16766)
-- Name: department_operation_claims; Type: TABLE; Schema: public; Owner: -
--

CREATE TABLE public.department_operation_claims (
    id bigint NOT NULL,
    department_id integer NOT NULL,
    operation_claim_id integer NOT NULL
);


--
-- TOC entry 213 (class 1259 OID 16764)
-- Name: department_operation_claims_id_seq; Type: SEQUENCE; Schema: public; Owner: -
--

CREATE SEQUENCE public.department_operation_claims_id_seq
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


--
-- TOC entry 3109 (class 0 OID 0)
-- Dependencies: 213
-- Name: department_operation_claims_id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: -
--

ALTER SEQUENCE public.department_operation_claims_id_seq OWNED BY public.department_operation_claims.id;


--
-- TOC entry 212 (class 1259 OID 16740)
-- Name: departments; Type: TABLE; Schema: public; Owner: -
--

CREATE TABLE public.departments (
    id integer NOT NULL,
    name character varying(50) NOT NULL,
    description text,
    operation_claim_names character varying(20)[] NOT NULL,
    is_enable boolean DEFAULT true,
    created_at timestamp with time zone DEFAULT CURRENT_TIMESTAMP,
    updated_at timestamp with time zone DEFAULT CURRENT_TIMESTAMP
);


--
-- TOC entry 211 (class 1259 OID 16738)
-- Name: departments_id_seq; Type: SEQUENCE; Schema: public; Owner: -
--

CREATE SEQUENCE public.departments_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


--
-- TOC entry 3110 (class 0 OID 0)
-- Dependencies: 211
-- Name: departments_id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: -
--

ALTER SEQUENCE public.departments_id_seq OWNED BY public.departments.id;


--
-- TOC entry 208 (class 1259 OID 16720)
-- Name: fixture_positions; Type: TABLE; Schema: public; Owner: -
--

CREATE TABLE public.fixture_positions (
    id smallint NOT NULL,
    name character varying(15) NOT NULL
);


--
-- TOC entry 201 (class 1259 OID 16530)
-- Name: fixtures; Type: TABLE; Schema: public; Owner: -
--

CREATE TABLE public.fixtures (
    id uuid DEFAULT uuid_generate_v4() NOT NULL,
    name character varying(50) NOT NULL,
    description text,
    picture_url text,
    date_purchase date NOT NULL,
    date_warranty date NOT NULL,
    price money DEFAULT 0,
    created_at timestamp with time zone DEFAULT CURRENT_TIMESTAMP,
    updated_at timestamp with time zone DEFAULT CURRENT_TIMESTAMP,
    supplier_id integer NOT NULL,
    category_id smallint NOT NULL,
    fixture_position_id smallint NOT NULL
);


--
-- TOC entry 215 (class 1259 OID 16782)
-- Name: logs; Type: TABLE; Schema: public; Owner: -
--

CREATE TABLE public.logs (
    id uuid DEFAULT uuid_generate_v4() NOT NULL,
    detail text NOT NULL,
    method_name character varying(100) NOT NULL,
    level character varying(20) NOT NULL,
    claim_id character(36) NOT NULL,
    date timestamp with time zone DEFAULT CURRENT_TIMESTAMP NOT NULL
);


--
-- TOC entry 210 (class 1259 OID 16732)
-- Name: operation_claims; Type: TABLE; Schema: public; Owner: -
--

CREATE TABLE public.operation_claims (
    id integer NOT NULL,
    name character varying(50) NOT NULL,
    description character varying(100) NOT NULL
);


--
-- TOC entry 209 (class 1259 OID 16730)
-- Name: operation_claims_id_seq; Type: SEQUENCE; Schema: public; Owner: -
--

CREATE SEQUENCE public.operation_claims_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


--
-- TOC entry 3111 (class 0 OID 0)
-- Dependencies: 209
-- Name: operation_claims_id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: -
--

ALTER SEQUENCE public.operation_claims_id_seq OWNED BY public.operation_claims.id;


--
-- TOC entry 205 (class 1259 OID 16571)
-- Name: suppliers; Type: TABLE; Schema: public; Owner: -
--

CREATE TABLE public.suppliers (
    id integer NOT NULL,
    name character varying(50) NOT NULL,
    description text,
    is_enable boolean DEFAULT true,
    created_at timestamp with time zone DEFAULT CURRENT_TIMESTAMP,
    updated_at timestamp with time zone DEFAULT CURRENT_TIMESTAMP
);


--
-- TOC entry 204 (class 1259 OID 16569)
-- Name: suppliers_id_seq; Type: SEQUENCE; Schema: public; Owner: -
--

CREATE SEQUENCE public.suppliers_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


--
-- TOC entry 3112 (class 0 OID 0)
-- Dependencies: 204
-- Name: suppliers_id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: -
--

ALTER SEQUENCE public.suppliers_id_seq OWNED BY public.suppliers.id;


--
-- TOC entry 206 (class 1259 OID 16602)
-- Name: users; Type: TABLE; Schema: public; Owner: -
--

CREATE TABLE public.users (
    id uuid DEFAULT uuid_generate_v4() NOT NULL,
    first_name character varying(50) NOT NULL,
    last_name character varying(50) NOT NULL,
    email character varying(50) NOT NULL,
    username character varying(20) NOT NULL,
    birth_date date,
    password_salt bytea NOT NULL,
    password_hash bytea NOT NULL,
    is_enable boolean DEFAULT true,
    created_at timestamp with time zone DEFAULT CURRENT_TIMESTAMP,
    updated_at timestamp with time zone DEFAULT CURRENT_TIMESTAMP,
    department_id integer NOT NULL
);

--
-- TOC entry 2916 (class 2604 OID 16556)
-- Name: categories id; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.categories ALTER COLUMN id SET DEFAULT nextval('public.categories_id_seq'::regclass);


--
-- TOC entry 2937 (class 2604 OID 16769)
-- Name: department_operation_claims id; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.department_operation_claims ALTER COLUMN id SET DEFAULT nextval('public.department_operation_claims_id_seq'::regclass);


--
-- TOC entry 2933 (class 2604 OID 16743)
-- Name: departments id; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.departments ALTER COLUMN id SET DEFAULT nextval('public.departments_id_seq'::regclass);


--
-- TOC entry 2932 (class 2604 OID 16735)
-- Name: operation_claims id; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.operation_claims ALTER COLUMN id SET DEFAULT nextval('public.operation_claims_id_seq'::regclass);


--
-- TOC entry 2920 (class 2604 OID 16574)
-- Name: suppliers id; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.suppliers ALTER COLUMN id SET DEFAULT nextval('public.suppliers_id_seq'::regclass);


--
-- TOC entry 2943 (class 2606 OID 16564)
-- Name: categories categories_pkey; Type: CONSTRAINT; Schema: public; Owner: -
--

ALTER TABLE ONLY public.categories
    ADD CONSTRAINT categories_pkey PRIMARY KEY (id);


--
-- TOC entry 2953 (class 2606 OID 16637)
-- Name: debits debits_pkey; Type: CONSTRAINT; Schema: public; Owner: -
--

ALTER TABLE ONLY public.debits
    ADD CONSTRAINT debits_pkey PRIMARY KEY (id);


--
-- TOC entry 2961 (class 2606 OID 16771)
-- Name: department_operation_claims department_operation_claims_pkey; Type: CONSTRAINT; Schema: public; Owner: -
--

ALTER TABLE ONLY public.department_operation_claims
    ADD CONSTRAINT department_operation_claims_pkey PRIMARY KEY (id);


--
-- TOC entry 2959 (class 2606 OID 16748)
-- Name: departments departments_pkey; Type: CONSTRAINT; Schema: public; Owner: -
--

ALTER TABLE ONLY public.departments
    ADD CONSTRAINT departments_pkey PRIMARY KEY (id);


--
-- TOC entry 2955 (class 2606 OID 16724)
-- Name: fixture_positions fixture_positions_pkey; Type: CONSTRAINT; Schema: public; Owner: -
--

ALTER TABLE ONLY public.fixture_positions
    ADD CONSTRAINT fixture_positions_pkey PRIMARY KEY (id);


--
-- TOC entry 2941 (class 2606 OID 16542)
-- Name: fixtures fixtures_pkey; Type: CONSTRAINT; Schema: public; Owner: -
--

ALTER TABLE ONLY public.fixtures
    ADD CONSTRAINT fixtures_pkey PRIMARY KEY (id);


--
-- TOC entry 2963 (class 2606 OID 16791)
-- Name: logs logs_pkey; Type: CONSTRAINT; Schema: public; Owner: -
--

ALTER TABLE ONLY public.logs
    ADD CONSTRAINT logs_pkey PRIMARY KEY (id);


--
-- TOC entry 2957 (class 2606 OID 16737)
-- Name: operation_claims operation_claims_pkey; Type: CONSTRAINT; Schema: public; Owner: -
--

ALTER TABLE ONLY public.operation_claims
    ADD CONSTRAINT operation_claims_pkey PRIMARY KEY (id);


--
-- TOC entry 2945 (class 2606 OID 16582)
-- Name: suppliers suppliers_pkey; Type: CONSTRAINT; Schema: public; Owner: -
--

ALTER TABLE ONLY public.suppliers
    ADD CONSTRAINT suppliers_pkey PRIMARY KEY (id);


--
-- TOC entry 2947 (class 2606 OID 16615)
-- Name: users users_email_key; Type: CONSTRAINT; Schema: public; Owner: -
--

ALTER TABLE ONLY public.users
    ADD CONSTRAINT users_email_key UNIQUE (email);


--
-- TOC entry 2949 (class 2606 OID 16613)
-- Name: users users_pkey; Type: CONSTRAINT; Schema: public; Owner: -
--

ALTER TABLE ONLY public.users
    ADD CONSTRAINT users_pkey PRIMARY KEY (id);


--
-- TOC entry 2951 (class 2606 OID 16617)
-- Name: users users_username_key; Type: CONSTRAINT; Schema: public; Owner: -
--

ALTER TABLE ONLY public.users
    ADD CONSTRAINT users_username_key UNIQUE (username);


--
-- TOC entry 2968 (class 2606 OID 16655)
-- Name: debits debits_fixture_id_fkey; Type: FK CONSTRAINT; Schema: public; Owner: -
--

ALTER TABLE ONLY public.debits
    ADD CONSTRAINT debits_fixture_id_fkey FOREIGN KEY (fixture_id) REFERENCES public.fixtures(id) NOT VALID;


--
-- TOC entry 2969 (class 2606 OID 16660)
-- Name: debits debits_user_id_fkey; Type: FK CONSTRAINT; Schema: public; Owner: -
--

ALTER TABLE ONLY public.debits
    ADD CONSTRAINT debits_user_id_fkey FOREIGN KEY (user_id) REFERENCES public.users(id) NOT VALID;


--
-- TOC entry 2970 (class 2606 OID 16772)
-- Name: department_operation_claims department_operation_claims_department_id_fkey; Type: FK CONSTRAINT; Schema: public; Owner: -
--

ALTER TABLE ONLY public.department_operation_claims
    ADD CONSTRAINT department_operation_claims_department_id_fkey FOREIGN KEY (department_id) REFERENCES public.departments(id) NOT VALID;


--
-- TOC entry 2971 (class 2606 OID 16777)
-- Name: department_operation_claims department_operation_claims_operation_claim_id_fkey; Type: FK CONSTRAINT; Schema: public; Owner: -
--

ALTER TABLE ONLY public.department_operation_claims
    ADD CONSTRAINT department_operation_claims_operation_claim_id_fkey FOREIGN KEY (operation_claim_id) REFERENCES public.operation_claims(id) NOT VALID;


--
-- TOC entry 2965 (class 2606 OID 16675)
-- Name: fixtures fixtures_category_id_fkey; Type: FK CONSTRAINT; Schema: public; Owner: -
--

ALTER TABLE ONLY public.fixtures
    ADD CONSTRAINT fixtures_category_id_fkey FOREIGN KEY (category_id) REFERENCES public.categories(id) NOT VALID;


--
-- TOC entry 2966 (class 2606 OID 16725)
-- Name: fixtures fixtures_fixture_position_id_fkey; Type: FK CONSTRAINT; Schema: public; Owner: -
--

ALTER TABLE ONLY public.fixtures
    ADD CONSTRAINT fixtures_fixture_position_id_fkey FOREIGN KEY (fixture_position_id) REFERENCES public.fixture_positions(id) NOT VALID;


--
-- TOC entry 2964 (class 2606 OID 16670)
-- Name: fixtures fixtures_supplier_id_fkey; Type: FK CONSTRAINT; Schema: public; Owner: -
--

ALTER TABLE ONLY public.fixtures
    ADD CONSTRAINT fixtures_supplier_id_fkey FOREIGN KEY (supplier_id) REFERENCES public.suppliers(id) NOT VALID;


--
-- TOC entry 2967 (class 2606 OID 16749)
-- Name: users users_department_id_fkey; Type: FK CONSTRAINT; Schema: public; Owner: -
--

ALTER TABLE ONLY public.users
    ADD CONSTRAINT users_department_id_fkey FOREIGN KEY (department_id) REFERENCES public.departments(id) NOT VALID;


-- Completed on 2021-01-21 22:09:15

--
-- PostgreSQL database dump complete
--

